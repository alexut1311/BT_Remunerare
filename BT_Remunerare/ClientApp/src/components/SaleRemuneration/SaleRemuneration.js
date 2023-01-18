import React, { Component } from "react";
import httpClient from "../../utils/httpClient";
import { ApplicationTable } from "../Table/ApplicationTable";
import { CreateModal } from "../Modal/CreateModal";
import {
  MODAL_CANCEL_TEXT,
  REMUNERATION_HEADER_TEXT,
  REMUNERATION_MODAL_TEXT,
} from "../../utils/constValues";

export class SaleRemuneration extends Component {
  static displayName = SaleRemuneration.name;

  constructor(props) {
    super(props);
    this.state = {
      salesRemunerations: [],
      loading: true,
      remunerationId: 0,
      periodId: 0,
      productId: 0,
      remuneration: 0,
      createModalOpen: false,
    };
    this.renderPeriodsTable = this.renderPeriodsTable.bind(this);
  }

  componentDidMount() {
    this.populatePeriodData();
  }

  renderPeriodsTable(salesRemunerations) {
    const handleDeleteRow = (row) => {};

    const columns = [
      {
        accessorKey: "remunerationId",
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
        accessorKey: "salesRemunerationPeriod.year",
        header: "An",
        size: 100,
      },
      {
        accessorKey: "salesRemunerationPeriod.month",
        header: "Luna",
        size: 100,
      },
      {
        accessorKey: "salesRemunerationProduct.productName",
        header: "Produs",
        size: 140,
      },
      {
        accessorKey: "remuneration",
        header: "Remuneratie",
        size: 100,
        type: "numeric",
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
        modalText={REMUNERATION_MODAL_TEXT}
        modalCancelText={MODAL_CANCEL_TEXT}
        setComponentState={setComponentState}
        //onSubmit={handleCreateNewRow}
      />
    );

    const applicationTable = (
      <ApplicationTable
        columns={columns}
        data={salesRemunerations}
        handleDeleteRow={handleDeleteRow}
        modalText={REMUNERATION_MODAL_TEXT}
        openModal={openModal}
        initialState={{ columnVisibility: { remunerationId: false } }}
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
      this.renderPeriodsTable(this.state.salesRemunerations)
    );

    return (
      <div>
        <h1>{REMUNERATION_HEADER_TEXT}</h1>
        {contents}
      </div>
    );
  }

  async populatePeriodData() {
    const response = await httpClient.get(
      "/salesRemunerations/GetAllSalesRemunerationsWithProductAndPeriod"
    );
    const data = await response.json();
    this.setState({ salesRemunerations: data, loading: false });
  }
}
