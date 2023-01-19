import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { ApplicationTable } from "../Table/ApplicationTable";
import { CreateModal } from "../Modal/CreateModal";
import {
  MODAL_CANCEL_TEXT,
  REMUNERATION_EDIT_MODAL_TEXT,
  REMUNERATION_HEADER_TEXT,
  REMUNERATION_MODAL_TEXT,
} from "../../utils/constValues";
import { IconButton, MenuItem } from "@mui/material";
import { CreateNewSaleRemunerationModal } from "./CreateNewSaleRemunerationModal";
import { Edit } from "@mui/icons-material";

export class SaleRemuneration extends Component {
  static displayName = SaleRemuneration.name;

  constructor(props) {
    super(props);
    this.state = {
      salesRemunerations: [],
      loading: true,
      remunerationId: 0,
      remuneration: 0,
      periodId: 0,
      productId: 0,
      createModalOpen: false,
      editModalOpen: false,
      allSelectablePeriods: [],
      allProducts: [],
    };
    this.renderSaleRemunerationsTable =
      this.renderSaleRemunerationsTable.bind(this);

    this.createNewSaleRemuneration = this.createNewSaleRemuneration.bind(this);
    this.populateSaleRemunerationData =
      this.populateSaleRemunerationData.bind(this);
    this.deleteSaleRemunerationById =
      this.deleteSaleRemunerationById.bind(this);
  }

  componentDidMount() {
    this.populateSaleRemunerationData();
  }

  renderSaleRemunerationsTable(salesRemunerations) {
    const handleDeleteRow = (row) => {
      var result = window.confirm(
        `Esti sigur ca vrei sa stergi remunerarea produsului ${row.original.salesRemunerationProduct.productName}?`
      );
      if (result === true) {
        this.deleteSaleRemunerationById(row.original.remunerationId);
      }
    };

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

    const createSaleRemunerationModal = (
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

    const editSaleRemunerationModal = (
      <CreateNewSaleRemunerationModal
        periodId={this.state.periodId}
        productId={this.state.productId}
        periods={this.state.allSelectablePeriods}
        products={this.state.allProducts}
        open={this.state.editModalOpen}
        onClose={() => this.setState({ editModalOpen: false })}
        modalText={REMUNERATION_EDIT_MODAL_TEXT}
        modalCancelText={MODAL_CANCEL_TEXT}
        onSubmit={this.createNewSaleRemuneration}
        setComponentState={setComponentState}
      />
    );

    const editButton = (row) => {
      this.setState({ periodId: row.periodId, productId: row.productId });
      return (
        <IconButton onClick={() => this.setState({ editModalOpen: true })}>
          <Edit />
        </IconButton>
      );
    };

    const applicationTable = (
      <ApplicationTable
        columns={columns}
        data={salesRemunerations}
        handleDeleteRow={handleDeleteRow}
        modalText={REMUNERATION_MODAL_TEXT}
        openModal={openModal}
        initialState={{ columnVisibility: { remunerationId: false } }}
        editButton={editButton}
      />
    );

    return (
      <>
        {applicationTable}
        {createSaleRemunerationModal}
        {editSaleRemunerationModal}
      </>
    );
  }

  render() {
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
    this.setState({ loading: true });
    this.populateSaleRemunerationData();
  }

  async deleteSaleRemunerationById(remunerationId) {
    const response = await httpClient.delete(
      "/salesRemunerations/DeleteSalesRemuneration",
      remunerationId
    );
    this.setState({ loading: true });
    this.populateSaleRemunerationData();
  }
}
