import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { ApplicationTable } from "../Table/ApplicationTable";
import { CreateModal } from "../Modal/CreateModal";
import {
  MODAL_CANCEL_TEXT,
  REMUNERATION_HEADER_TEXT,
  REMUNERATION_MODAL_TEXT,
} from "../../utils/constValues";
import { MenuItem } from "@mui/material";
import { CreateNewSaleRemunerationModal } from "./CreateNewSaleRemunerationModal";

export class SaleRemuneration extends Component {
  static displayName = SaleRemuneration.name;

  constructor(props) {
    super(props);
    this.state = {
      salesRemunerations: [],
      loading: true,
      remunerationId: 0,
      remuneration: 0,
      createModalOpen: false,
      allSelectablePeriods: [],
      allProducts: [],
    };
    this.renderSaleRemunerationsTable =
      this.renderSaleRemunerationsTable.bind(this);

    this.createNewSaleRemuneration = this.createNewSaleRemuneration.bind(this);
    this.populateSaleRemunerationData =
      this.populateSaleRemunerationData.bind(this);
  }

  renderSaleRemunerationsTable(salesRemunerations) {
    const handleDeleteRow = (row) => {};

    const columns = [
      {
        accessorKey: "remunerationId",
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
        accessorKey: "salesRemunerationPeriod.year",
        header: "An",
        size: 100,
      },
      {
        accessorKey: "salesRemunerationPeriod.month",
        header: "Luna",
        size: 100,
      },
      {
        accessorKey: "salesRemunerationProduct.productName",
        header: "Produs",
        size: 140,
      },
      {
        accessorKey: "remuneration",
        header: "Remuneratie",
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
      <CreateNewSaleRemunerationModal
        periods={this.state.allSelectablePeriods}
        products={this.state.allProducts}
        open={this.state.createModalOpen}
        onClose={() => this.setState({ createModalOpen: false })}
        modalText={REMUNERATION_MODAL_TEXT}
        modalCancelText={MODAL_CANCEL_TEXT}
        onSubmit={this.createNewSaleRemuneration}
        setComponentState={setComponentState}
      />
    );

    const applicationTable = (
      <ApplicationTable
        columns={columns}
        data={salesRemunerations}
        handleDeleteRow={handleDeleteRow}
        modalText={REMUNERATION_MODAL_TEXT}
        openModal={openModal}
        initialState={{ columnVisibility: { remunerationId: false } }}
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
    this.populateSaleRemunerationData();
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderSaleRemunerationsTable(this.state.salesRemunerations)
    );

    return (
      <div>
        <h1>{REMUNERATION_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populateSaleRemunerationData() {
    const salesRemunerations = await httpClient
      .get("/salesRemunerations/GetAllSalesRemunerationsWithProductAndPeriod")
      .then((res) => res.json());

    const allPeriodsResponse = await httpClient
      .get("/period/GetAllPeriods")
      .then((res) => res.json());

    const allProductsResponse = await httpClient
      .get("/product/GetAllProducts")
      .then((res) => res.json());

    this.setState({
      salesRemunerations: salesRemunerations,
      allSelectablePeriods: allPeriodsResponse,
      allProducts: allProductsResponse,
      periodId: allPeriodsResponse[0]?.periodId,
      productId: allProductsResponse[0]?.productId,
      loading: false,
    });
  }

  async createNewSaleRemuneration(saleRemuneration) {
    const response = await httpClient.post(
      "/salesRemunerations/AddSalesRemuneration",
      {
        periodId: saleRemuneration.periodId,
        productId: saleRemuneration.productId,
        remuneration: this.state.remuneration,
      }
    );
  }
}
