import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { ApplicationTable } from "../Table/ApplicationTable";
import { CreateModal } from "../Modal/CreateModal";
import {
  MODAL_CANCEL_TEXT,
  PRODUCT_HEADER_TEXT,
  PRODUCT_MODAL_TEXT,
} from "../../utils/constValues";

export class Product extends Component {
  static displayName = Product.name;

  constructor(props) {
    super(props);
    this.state = {
      products: [],
      loading: true,
      productId: 0,
      productName: "",
      createModalOpen: false,
    };
    this.renderPeriodsTable = this.renderPeriodsTable.bind(this);
  }

  componentDidMount() {
    this.populatePeriodData();
  }

  renderPeriodsTable(products) {
    const handleDeleteRow = (row) => {};

    const columns = [
      {
        accessorKey: "productId",
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
        accessorKey: "productName",
        header: "Nume Produs",
        size: 140,
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
        modalText={PRODUCT_MODAL_TEXT}
        modalCancelText={MODAL_CANCEL_TEXT}
        setComponentState={setComponentState}
        //onSubmit={handleCreateNewRow}
      />
    );

    const applicationTable = (
      <ApplicationTable
        columns={columns}
        data={products}
        handleDeleteRow={handleDeleteRow}
        modalText={PRODUCT_MODAL_TEXT}
        openModal={openModal}
        initialState={{ columnVisibility: { productId: false } }}
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
      this.renderPeriodsTable(this.state.products)
    );

    return (
      <div>
        <h1>{PRODUCT_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populatePeriodData() {
    const response = await httpClient.get("/product/GetAllProducts");
    const data = await response.json();
    this.setState({ products: data, loading: false });
  }
}