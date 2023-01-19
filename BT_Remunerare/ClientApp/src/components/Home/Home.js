import React, { useState, useEffect, createElement } from "react";
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

export function Home() {
  const [period, setPeriod] = useState([]);
  const [allSelectablePeriods, setAllSelectablePeriods] = useState([]);
  const [totalSales, setTotalSales] = useState([]);
  const [loading, setLoading] = useState(true);
  const [periodId, setPeriodId] = useState(0);

  useEffect(() => {
    populatePeriodData(
      setPeriod,
      setLoading,
      setPeriodId,
      setAllSelectablePeriods
    );
  }, []);

  useEffect(() => {
    populateTotalSales(periodId, setTotalSales);
    setPeriod(allSelectablePeriods.find((x) => x.periodId === periodId));
  }, [periodId]);

  const renderTotalSalesTable = (totalSales) => {
    const createTableBody = (totalSales) => {
      if (totalSales.length === 0) {
        return createElement(
          "h3",
          { className: "centered-text" },
          "Nu s-au gasit inregistrari pentru aceasta perioada."
        );
      }
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
            [productNameCell, ...vendorSaleCells]
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
  };

  const createSelectMenu = (periods) => {
    const handleChange = (event) => {
      setPeriodId(event.target.value);
    };

    return (
      <Box sx={{ minWidth: 120 }}>
        <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
          <InputLabel id="period-dropdown-select-label">Perioada</InputLabel>
          <Select
            labelId="period-dropdown-select-label"
            id="period-dropdown-select"
            value={periodId}
            label="Perioada"
            onChange={handleChange}
          >
            {periods.length != 0 ? (
              periods.map((period) => (
                <MenuItem value={period.periodId}>
                  Anul {period.year} si luna {period.month}
                </MenuItem>
              ))
            ) : (
              <></>
            )}
          </Select>
        </FormControl>
      </Box>
    );
  };

  let contents = loading ? (
    <p>
      <em>Loading...</em>
    </p>
  ) : (
    renderTotalSalesTable(totalSales)
  );

  return (
    <div>
      <h1>BT Remunerare Challenge</h1>
      <h2>
        Tabel vanzari totale in anul {period?.year} si luna {period?.month}
      </h2>
      {createSelectMenu(allSelectablePeriods)}
      {contents}
    </div>
  );
}

async function populatePeriodData(
  setPeriod,
  setLoading,
  setPeriodId,
  setAllSelectablePeriods
) {
  const allPeriodsResponse = await httpClient
    .get("/period/GetAllPeriods")
    .then((res) => res.json());
  setPeriod(allPeriodsResponse[0]);
  setLoading(false);
  setPeriodId(allPeriodsResponse[0]?.periodId);
  setAllSelectablePeriods(allPeriodsResponse);
}

async function populateTotalSales(periodId, setTotalSales) {
  if (periodId === 0) {
    return;
  }

  const saleResponse = await httpClient
    .get(`/sale/GetTotalSalesValueByPeriodId/${periodId}`)
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
  setTotalSales(allSales);
}
