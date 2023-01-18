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
    this.renderPeriodsTable = this.renderPeriodsTable.bind(this);
  }

  componentDidMount() {
    this.populatePeriodData();
  }

  renderPeriodsTable(vendors) {
    const handleDeleteRow = (row) => {};

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
        //onSubmit={handleCreateNewRow}
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
      this.renderPeriodsTable(this.state.vendors)
    );

    return (
      <div>
        <h1>{VENDOR_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populatePeriodData() {
    const response = await httpClient.get("/vendor/GetAllVendors");
    const data = await response.json();
    this.setState({ vendors: data, loading: false });
  }
}
