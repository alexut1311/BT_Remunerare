import React, { Component, createElement } from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import httpClient from "../../utils/httpClient";
import Box from "@mui/material/Box";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import "./Home.css";

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
      uniqueVendors: [],
    };
    this.renderTotalSalesTable = this.renderTotalSalesTable.bind(this);
  }

  componentDidMount() {
    this.populatePeriodData();
    this.pop();
  }

  renderTotalSalesTable(totalSales) {
    const createTableBody = (totalSales) => {
      let vendorHeadings = [createElement(TableCell, null, "")];
      let productRows = [];
      let uniqueVendors = new Set();
      totalSales.forEach((element, index) => {
        element.vendorSales
          .map((x) => x.vendorName)
          .forEach((item, index) => {
            uniqueVendors.add(item);
          });
      });
      uniqueVendors = Array.from(uniqueVendors);
      uniqueVendors.forEach((vendor, index) => {
        vendorHeadings.push(
          createElement(
            TableCell,
            { className: "heading-text-style" },
            `${vendor}`
          )
        );
      });
      let tableHeadingRow = createElement(
        TableRow,
        {
          key: "vendors",
        },
        vendorHeadings
      );
      let tableHeading = createElement(TableHead, null, tableHeadingRow);

      totalSales.forEach((sale, index) => {
        let productNameCell = createElement(
          TableCell,
          { className: "heading-text-style" },
          `${sale.productName}`
        );
        let vendorSaleCells = [];
        sale.vendorSales.forEach((vendorSale, vendorSaleIndex) => {
          if (vendorSale.vendorName === uniqueVendors[vendorSaleIndex]) {
            vendorSaleCells.push(
              createElement(TableCell, null, `${vendorSale.totalSale}`)
            );
          }
        });
        productRows.push(
          createElement(
            TableRow,
            {
              key: sale.productName,
            },
            [productNameCell, vendorSaleCells]
          )
        );
      });

      let tableBody = createElement(TableBody, null, productRows);

      return createElement(Table, { sx: { minWidth: 650 } }, [
        tableHeading,
        tableBody,
      ]);
    };

    return (
      <>
        <TableContainer component={Paper}>
          {createTableBody(totalSales)}
        </TableContainer>
      </>
    );
  }

  createSelectMenu(periods) {
    const handleChange = (event) => {
      this.setState({ periodId: event.target.value });
    };

    return (
      <Box sx={{ minWidth: 120 }}>
        <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
          <InputLabel id="period-dropdown-select-label">Perioada</InputLabel>
          <Select
            labelId="period-dropdown-select-label"
            id="period-dropdown-select"
            value={this.state.periodId}
            label="Perioada"
            onChange={handleChange}
          >
            {periods.map((period) => (
              <MenuItem value={period.periodId}>
                Anul {period.year} si luna {period.month}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      </Box>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderTotalSalesTable(this.state.totalSales)
    );

    return (
      <div>
        <h1>BT Remunerare Challenge</h1>
        <h2>
          Tabel vanzari totale in anul {this.state.period?.year} si luna{" "}
          {this.state.period?.month}
        </h2>
        {this.createSelectMenu(this.state.allSelectablePeriods)}
        {contents}
      </div>
    );
  }

  async populatePeriodData() {
    const allPeriodsResponse = await httpClient
      .get("/period/GetAllPeriods")
      .then((res) => res.json());

    this.setState({
      period: allPeriodsResponse[0],
      loading: false,
      periodId: allPeriodsResponse[0]?.periodId,
      allSelectablePeriods: allPeriodsResponse,
    });
  }
  async pop() {
    if (this.state.periodId === 0) {
      return;
    }

    const saleResponse = await httpClient
      .get(`/sale/GetTotalSalesValueByPeriodId/${this.state.periodId}`)
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
      totalSales: allSales,
    });
  }
}
