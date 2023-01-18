import React, { Component } from "react";
import MaterialReactTable from "material-react-table";
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
import { Delete, Edit } from "@mui/icons-material";

export class ApplicationTable extends Component {
  static displayName = ApplicationTable.name;

  constructor(props) {
    super(props);
    this.state = { createModalOpen: false };
  }

  render() {
    return (
      <>
        <MaterialReactTable
          columns={this.props.columns}
          displayColumnDefOptions={{
            "mrt-row-actions": {
              muiTableHeadCellProps: {
                align: "left",
              },
              size: 40,
              header: "Actiuni",
            },
          }}
          data={this.props.data}
          editingMode="modal" //default
          enableColumnOrdering
          enableEditing
          //onEditingRowSave={handleSaveRowEdits}
          //onEditingRowCancel={handleCancelRowEdits}
          renderRowActions={({ row, table }) => (
            <Box sx={{ display: "flex", gap: "1rem" }}>
              <Tooltip arrow placement="left" title="Edit">
                <IconButton onClick={() => table.setEditingRow(row)}>
                  <Edit />
                </IconButton>
              </Tooltip>
              <Tooltip arrow placement="right" title="Delete">
                <IconButton
                  color="error"
                  onClick={() => this.props.handleDeleteRow(row)}
                >
                  <Delete />
                </IconButton>
              </Tooltip>
            </Box>
          )}
          renderTopToolbarCustomActions={() => (
            <Button
              color="secondary"
              onClick={this.props.openModal}
              variant="contained"
            >
              {this.props.modalText}
            </Button>
          )}
          initialState={this.props.initialState}
        />
      </>
    );
  }
}
