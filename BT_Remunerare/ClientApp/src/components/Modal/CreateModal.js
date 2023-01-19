import React, { Component } from "react";
import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  MenuItem,
  Stack,
  TextField,
  Tooltip,
} from "@mui/material";

export class CreateModal extends Component {
  static displayName = CreateModal.name;

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
              {this.props.columns.map((column) =>
                column.isDisabledToEditing ? (
                  <></>
                ) : (
                  <TextField
                    key={column.accessorKey}
                    label={column.header}
                    name={column.accessorKey}
                    disabled={column.isDisabledToEditing}
                    onChange={(e) => this.props.setComponentState(e)}
                  />
                )
              )}
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
