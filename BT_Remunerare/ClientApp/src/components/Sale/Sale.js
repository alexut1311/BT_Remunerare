import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { ApplicationTable } from "../Table/ApplicationTable";
import { CreateModal } from "../Modal/CreateModal";
import {
  MODAL_CANCEL_TEXT,
  SALE_HEADER_TEXT,
  SALE_MODAL_TEXT,
} from "../../utils/constValues";

export class Sale extends Component {
  static displayName = Sale.name;

  constructor(props) {
    super(props);
    this.state = {
      sales: [],
      loading: true,
      saleId: 0,
      periodId: 0,
      vendorId: 0,
      productId: 0,
      numberOfProducts: 0,
      createModalOpen: false,
    };
    this.renderPeriodsTable = this.renderPeriodsTable.bind(this);
  }

  componentDidMount() {
    this.populatePeriodData();
  }

  renderPeriodsTable(sales) {
    const handleDeleteRow = (row) => {};

    const columns = [
      {
        accessorKey: "saleId",
        header: "ID",
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
        accessorKey: "salePeriod.year",
        header: "An",
        size: 100,
      },

      {
        accessorKey: "salePeriod.month",
        header: "Luna",
        size: 100,
      },

      {
        accessorKey: "saleVendor.vendorName",
        header: "Vanzator",
        size: 140,
      },

      {
        accessorKey: "saleProduct.productName",
        header: "Produs",
        size: 140,
      },

      {
        accessorKey: "numberOfProducts",
        header: "Numar produse",
        size: 100,
        type: "numeric",
      },
    ];

    const openModal = () => {
      this.setState({ createModalOpen: true });
    };

    const setComponentState = (e) => {
      this.setState({ [e.target.name]: e.target.value });
    };

    const productModal = (
      <CreateModal
        columns={columns}
        open={this.state.createModalOpen}
        onClose={() => this.setState({ createModalOpen: false })}
        modalText={SALE_MODAL_TEXT}
        modalCancelText={MODAL_CANCEL_TEXT}
        setComponentState={setComponentState}
        //onSubmit={handleCreateNewRow}
      />
    );

    const applicationTable = (
      <ApplicationTable
        columns={columns}
        data={sales}
        handleDeleteRow={handleDeleteRow}
        modalText={SALE_MODAL_TEXT}
        openModal={openModal}
        initialState={{ columnVisibility: { saleId: false } }}
      />
    );

    return (
      <>
        {applicationTable}
        {productModal}
      </>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderPeriodsTable(this.state.sales)
    );

    return (
      <div>
        <h1>{SALE_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populatePeriodData() {
    const response = await httpClient.get(
      "/sale/GetAllSalesWithVendorAndProductAndPeriod"
    );
    const data = await response.json();
    this.setState({ sales: data, loading: false });
  }
}
