import React, { Component } from "react";
import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  MenuItem,
  Stack,
  TextField,
} from "@mui/material";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";

export class CreateNewSaleModal extends Component {
  static displayName = CreateNewSaleModal.name;

  render() {
    const handleSubmit = () => {
      this.props.onSubmit();
      this.props.onClose();
    };

    let contents = (
      <Dialog open={this.props.open}>
        <DialogTitle textAlign="center">{this.props.modalText}</DialogTitle>
        <DialogContent>
          <form onSubmit={(e) => e.preventDefault()}>
            <Stack
              sx={{
                width: "100%",
                minWidth: { xs: "300px", sm: "360px", md: "400px" },
                gap: "1.5rem",
              }}
            >
              <Box sx={{ minWidth: 120 }}>
                <FormControl variant="standard" sx={{ m: 1, minWidth: 400 }}>
                  <InputLabel id="period-dropdown-select-label">
                    Perioada
                  </InputLabel>
                  <Select
                    labelId="period-dropdown-select-label"
                    id="period-dropdown-select"
                    label="Perioada"
                    name="periodId"
                    value={this.props.periodId}
                    onChange={(e) => this.props.setComponentState(e)}
                  >
                    {this.props.periods.map((period) => (
                      <MenuItem value={period.periodId} key={period.periodId}>
                        Anul {period.year} si luna {period.month}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Box>
              <Box sx={{ minWidth: 120 }}>
                <FormControl variant="standard" sx={{ m: 1, minWidth: 400 }}>
                  <InputLabel id="vendor-dropdown-select-label">
                    Vanzatorul
                  </InputLabel>
                  <Select
                    labelId="vendor-dropdown-select-label"
                    id="vendor-dropdown-select"
                    label="Vanzatorul"
                    name="vendorId"
                    value={this.props.vendorId}
                    onChange={(e) => this.props.setComponentState(e)}
                  >
                    {this.props.vendors.map((vendor) => (
                      <MenuItem value={vendor.vendorId} key={vendor.vendorId}>
                        {vendor.vendorName}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Box>
              <Box sx={{ minWidth: 120 }}>
                <FormControl variant="standard" sx={{ m: 1, minWidth: 400 }}>
                  <InputLabel id="period-dropdown-select-label">
                    Produsul
                  </InputLabel>
                  <Select
                    labelId="product-dropdown-select-label"
                    id="product-dropdown-select"
                    label="Produsul"
                    name="productId"
                    value={this.props.productId}
                    onChange={(e) => this.props.setComponentState(e)}
                  >
                    {this.props.products.map((product) => (
                      <MenuItem
                        value={product.productId}
                        key={product.productId}
                      >
                        {product.productName}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Box>
              <TextField
                key="numberOfProducts"
                label="Numar produse"
                name="numberOfProducts"
                type="number"
                value={this.props.numberOfProducts}
                onChange={(e) => this.props.setComponentState(e)}
              />
            </Stack>
          </form>
        </DialogContent>
        <DialogActions sx={{ p: "1.25rem" }}>
          <Button onClick={this.props.onClose}>
            {this.props.modalCancelText}
          </Button>
          <Button color="primary" onClick={handleSubmit} variant="contained">
            {this.props.modalText}
          </Button>
        </DialogActions>
      </Dialog>
    );

    return <div>{contents}</div>;
  }
}
