import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { ApplicationTable } from "../Table/ApplicationTable";
import { CreateModal } from "../Modal/CreateModal";
import {
  MODAL_CANCEL_TEXT,
  VENDOR_HEADER_TEXT,
  VENDOR_MODAL_TEXT,
} from "../../utils/constValues";

export class Vendor extends Component {
  static displayName = Vendor.name;

  constructor(props) {
    super(props);
    this.state = {
      vendors: [],
      loading: true,
      vendorId: 0,
      vendorName: "",
      createModalOpen: false,
    };
    this.renderVendorsTable = this.renderVendorsTable.bind(this);
    this.createNewVendor = this.createNewVendor.bind(this);
    this.populateVendorData = this.populateVendorData.bind(this);
    this.deleteVendorById = this.deleteVendorById.bind(this);
  }

  componentDidMount() {
    this.populateVendorData();
  }

  renderVendorsTable(vendors) {
    const handleDeleteRow = (row) => {
      var result = window.confirm(
        `Esti sigur ca vrei sa stergi vanzatorul ${row.original.vendorName}?`
      );
      if (result === true) {
        this.deleteVendorById(row.original.vendorId);
      }
    };

    const columns = [
      {
        accessorKey: "vendorId",
        header: "Id",
        enableColumnOrdering: false,
        enableEditing: false, //disable editing on this column
        enableSorting: false,
        editable: "never",
        enableHiding: false,
        hidden: true,
        size: 80,
        isDisabledToEditing: true,
      },
      {
        accessorKey: "vendorName",
        header: "Nume vanzator",
        size: 140,
      },
    ];

    const openModal = () => {
      this.setState({ createModalOpen: true });
    };

    const setComponentState = (e) => {
      this.setState({ [e.target.name]: e.target.value });
    };

    const vendorModal = (
      <CreateModal
        columns={columns}
        open={this.state.createModalOpen}
        onClose={() => this.setState({ createModalOpen: false })}
        modalText={VENDOR_MODAL_TEXT}
        modalCancelText={MODAL_CANCEL_TEXT}
        setComponentState={setComponentState}
        onSubmit={this.createNewVendor}
      />
    );

    const applicationTable = (
      <ApplicationTable
        columns={columns}
        data={vendors}
        handleDeleteRow={handleDeleteRow}
        modalText={VENDOR_MODAL_TEXT}
        openModal={openModal}
        initialState={{ columnVisibility: { vendorId: false } }}
      />
    );

    return (
      <>
        {applicationTable}
        {vendorModal}
      </>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderVendorsTable(this.state.vendors)
    );

    return (
      <div>
        <h1>{VENDOR_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populateVendorData() {
    const response = await httpClient.get("/vendor/GetAllVendors");
    const data = await response.json();
    this.setState({ vendors: data, loading: false });
  }

  async createNewVendor() {
    const response = await httpClient.post("/vendor/AddVendor", {
      vendorName: this.state.vendorName,
    });
    this.setState({ loading: true });
    this.populateVendorData();
  }

  async deleteVendorById(vendorId) {
    const response = await httpClient.delete("/vendor/DeleteVendor", vendorId);
    this.setState({ loading: true });
    this.populateVendorData();
  }
}
