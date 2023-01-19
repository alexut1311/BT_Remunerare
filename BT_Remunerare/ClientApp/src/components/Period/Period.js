import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { ApplicationTable } from "../Table/ApplicationTable";
import { CreateModal } from "../Modal/CreateModal";
import {
  MODAL_CANCEL_TEXT,
  PERIOD_HEADER_TEXT,
  PERIOD_MODAL_TEXT,
} from "../../utils/constValues";

export class Period extends Component {
  static displayName = Period.name;

  constructor(props) {
    super(props);
    this.state = {
      periods: [],
      loading: true,
      periodId: 0,
      year: 0,
      month: 0,
      createModalOpen: false,
    };
    this.renderPeriodsTable = this.renderPeriodsTable.bind(this);
  }

  componentDidMount() {
    this.populatePeriodData();
  }

  renderPeriodsTable(periods) {
    const handleDeleteRow = (row) => {};

    const columns = [
      {
        accessorKey: "periodId",
        header: "Id",
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

    const openModal = () => {
      this.setState({ createModalOpen: true });
    };

    const setComponentState = (e) => {
      this.setState({ [e.target.name]: e.target.value });
    };

    const periodModal = (
      <CreateModal
        columns={columns}
        open={this.state.createModalOpen}
        onClose={() => this.setState({ createModalOpen: false })}
        modalText={PERIOD_MODAL_TEXT}
        modalCancelText={MODAL_CANCEL_TEXT}
        setComponentState={setComponentState}
        //onSubmit={handleCreateNewRow}
      />
    );

    const applicationTable = (
      <ApplicationTable
        columns={columns}
        data={periods}
        handleDeleteRow={handleDeleteRow}
        modalText={PERIOD_MODAL_TEXT}
        openModal={openModal}
        initialState={{ columnVisibility: { periodId: false } }}
      />
    );

    return (
      <>
        {applicationTable}
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

    return (
      <div>
        <h1>{PERIOD_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populatePeriodData() {
    const response = await httpClient.get("/period/GetAllPeriods");
    const data = await response.json();
    this.setState({ periods: data, loading: false });
  }
}