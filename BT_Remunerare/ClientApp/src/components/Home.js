import React, { Component } from "react";
import httpClient from "../utils/httpClient";

export class Home extends Component {
  static displayName = Home.name;
  constructor(props) {
    super(props);
    this.state = {
      period: [],
      allSelectablePeriods: [],
      totalSales: [],
      loading: true,
      periodId: 0,
      createModalOpen: false,
    };
  }

  async componentDidMount() {
    await this.populatePeriodData();
  }
  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      <p></p>
      //this.renderTotalSalesTable(this.state.periods)
    );

    return (
      <div>
        <h1>BT Remunerare Challenge</h1>
        <h2>
          Tabel vanzari totale in anul {this.state.period?.year} si luna{" "}
          {this.state.period?.month}
        </h2>
        {contents}
      </div>
    );
  }

  async populatePeriodData() {
    const periodResponse = await httpClient
      .get("/period/GetPeriodById/1")
      .then((res) => res.json());
    const saleResponse = await httpClient
      .get(`/sale/GetTotalSalesValueByPeriodId/${periodResponse.periodId}`)
      .then((res) => res.json());
    let allSales = [];
    for (const [key, value] of Object.entries(saleResponse.totalSales)) {
      const uniqueProduct = [
        ...new Set(value.map((item) => item.product.productName)),
      ];
      const productName = uniqueProduct[0];
      const saleAgregation = {
        productName: productName,
        vendorSales: value
          .filter((x) => x.product.productName === productName)
          .map(function (item) {
            return {
              vendorName: item.vendor.vendorName,
              totalSale: item.totalSalesValue,
            };
          }),
      };
      allSales.push(saleAgregation);
    }
    this.setState({
      period: periodResponse,
      loading: false,
      periodId: periodResponse.periodId,
      totalSales: allSales,
    });
  }
}
