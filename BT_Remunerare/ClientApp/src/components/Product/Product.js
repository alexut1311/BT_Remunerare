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
    this.renderProductsTable = this.renderProductsTable.bind(this);
    this.createNewProduct = this.createNewProduct.bind(this);
    this.populateProductData = this.populateProductData.bind(this);
    this.deleteProductById = this.deleteProductById.bind(this);
    this.editProductById = this.editProductById.bind(this);
  }

  componentDidMount() {
    this.populateProductData();
  }

  renderProductsTable(products) {
    const handleDeleteRow = (row) => {
      var result = window.confirm(
        `Esti sigur ca vrei sa stergi produsul ${row.original.productName}?`
      );
      if (result == true) {
        this.deleteProductById(row.original.productId);
      }
    };

    const handleSaveRowEdits = (row) => {
      this.editProductById(row.values);
    };

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
        onSubmit={this.createNewProduct}
      />
    );

    const applicationTable = (
      <ApplicationTable
        columns={columns}
        data={products}
        handleDeleteRow={handleDeleteRow}
        modalText={PRODUCT_MODAL_TEXT}
        openModal={openModal}
        handleSaveRowEdits={handleSaveRowEdits}
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
      this.renderProductsTable(this.state.products)
    );

    return (
      <div>
        <h1>{PRODUCT_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populateProductData() {
    const response = await httpClient.get("/product/GetAllProducts");
    const data = await response.json();
    this.setState({ products: data, loading: false });
  }

  async createNewProduct() {
    const response = await httpClient.post("/product/AddProduct", {
      productName: this.state.productName,
    });
    this.setState({ loading: true });
    this.populateProductData();
  }

  async deleteProductById(productId) {
    const response = await httpClient.delete(
      "/product/DeleteProduct",
      productId
    );
    this.setState({ loading: true });
    this.populateProductData();
  }

  async editProductById(updatedProduct) {
    const response = await httpClient.post(
      "/product/UpdateProduct",
      updatedProduct
    );
    this.setState({ loading: true });
    this.populateProductData();
  }
}
