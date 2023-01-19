import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { ApplicationTable } from "../Table/ApplicationTable";
import { CreateModal } from "../Modal/CreateModal";
import {
  MODAL_CANCEL_TEXT,
  SALE_HEADER_TEXT,
  SALE_MODAL_TEXT,
} from "../../utils/constValues";
import { CreateNewSaleModal } from "./CreateNewSaleModal";

export class Sale extends Component {
  static displayName = Sale.name;

  constructor(props) {
    super(props);
    this.state = {
      sales: [],
      loading: true,
      periods: [],
      products: [],
      vendors: [],
      saleId: 0,
      periodId: 0,
      vendorId: 0,
      productId: 0,
      numberOfProducts: 0,
      createModalOpen: false,
    };
    this.renderSalesTable = this.renderSalesTable.bind(this);
    this.createNewSale = this.createNewSale.bind(this);
    this.populateSaleData = this.populateSaleData.bind(this);
    this.deleteSaleById = this.deleteSaleById.bind(this);
  }
  componentDidMount() {
    this.populateSaleData();
  }

  renderSalesTable(sales) {
    const handleDeleteRow = (row) => {
      var result = window.confirm(
        `Esti sigur ca vrei sa stergi vanzarea produsului ${row.original.saleProduct.productName}?`
      );
      if (result === true) {
        this.deleteSaleById(row.original.saleId);
      }
    };

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
      <CreateNewSaleModal
        columns={columns}
        periods={this.state.periods}
        products={this.state.products}
        vendors={this.state.vendors}
        open={this.state.createModalOpen}
        onClose={() => this.setState({ createModalOpen: false })}
        modalText={SALE_MODAL_TEXT}
        modalCancelText={MODAL_CANCEL_TEXT}
        setComponentState={setComponentState}
        onSubmit={this.createNewSale}
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
      this.renderSalesTable(this.state.sales)
    );

    return (
      <div>
        <h1>{SALE_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populateSaleData() {
    const allSalesResponse = await httpClient
      .get("/sale/GetAllSalesWithVendorAndProductAndPeriod")
      .then((res) => res.json());

    const allPeriodsResponse = await httpClient
      .get("/period/GetAllPeriods")
      .then((res) => res.json());

    const allVendorsResponse = await httpClient
      .get("/vendor/GetAllVendors")
      .then((res) => res.json());

    const allProductsResponse = await httpClient
      .get("/product/GetAllProducts")
      .then((res) => res.json());

    this.setState({
      sales: allSalesResponse,
      periods: allPeriodsResponse,
      vendors: allVendorsResponse,
      products: allProductsResponse,
      loading: false,
    });
  }

  async createNewSale(sale) {
    const response = await httpClient.post("/sale/AddSale", {
      periodId: sale.periodId,
      productId: sale.productId,
      vendorId: sale.productId,
      numberOfProducts: this.state.numberOfProducts,
    });
    this.setState({ loading: true });
    this.populateSaleData();
  }

  async deleteSaleById(saleId) {
    const response = await httpClient.delete("/sale/DeleteSale", saleId);
    this.setState({ loading: true });
    this.populateSaleData();
  }
}
