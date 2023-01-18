import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { CreateNewPeriodModal } from "./CreateNewPeriodModal";
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

export class Period extends Component {
  static displayName = Period.name;

  constructor(props) {
    super(props);
    this.state = { periods: [], loading: true, createModalOpen: false };
    this.renderPeriodsTable = this.renderPeriodsTable.bind(this);
  }

  componentDidMount() {
    this.populatePeriodData();
  }

  renderPeriodsTable(periods) {
    const handleDeleteRow = (row) => {};

    const columns = [
      {
        accessorKey: "id",
        header: "ID",
        enableColumnOrdering: false,
        enableEditing: false, //disable editing on this column
        enableSorting: false,
        hidden: true,
        size: 80,
      },
      {
        accessorKey: "year",
        header: "An",
        size: 140,
      },
      {
        accessorKey: "month",
        header: "Luna",
        size: 140,
      },
    ];
    const modalText = "Adauga o perioada noua";
    const modalCancelText = "Inchide";

    const periodModal = (
      <CreateNewPeriodModal
        columns={columns}
        open={this.state.createModalOpen}
        onClose={() => this.setState({ createModalOpen: false })}
        modalText={modalText}
        modalCancelText={modalCancelText}
        //onSubmit={handleCreateNewRow}
      />
    );

    return (
      <>
        <MaterialReactTable
          displayColumnDefOptions={{
            "mrt-row-actions": {
              muiTableHeadCellProps: {
                align: "center",
              },
              size: 120,
            },
          }}
          columns={columns}
          data={periods}
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
                <IconButton color="error" onClick={() => handleDeleteRow(row)}>
                  <Delete />
                </IconButton>
              </Tooltip>
            </Box>
          )}
          renderTopToolbarCustomActions={() => (
            <Button
              color="secondary"
              onClick={() => this.setState({ createModalOpen: true })}
              variant="contained"
            >
              {modalText}
            </Button>
          )}
          initialState={{ columnVisibility: { id: false } }}
        />
        {periodModal}
      </>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderPeriodsTable(this.state.periods)
    );

    return <div>{contents}</div>;
  }

  async populatePeriodData() {
    const response = await httpClient.get("/period/GetAllPeriods");
    const data = await response.json();
    this.setState({ periods: data, loading: false });
  }
}
