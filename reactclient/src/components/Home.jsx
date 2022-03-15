//import { render } from '@testing-library/react'
import React, { useState, useEffect } from 'react'
//import BugItLogo from './images/BugItLogo.jpg';
import './CSS/home.css'
import BootstrapTable from 'react-bootstrap-table-next'
import paginationFactory from 'react-bootstrap-table2-paginator'

const data = require('./data.json')

const columns = [
  {
    dataField: 'id',
    text: 'ID',
    sort: true,
  },
  {
    dataField: 'name',
    text: 'Name',
    sort: true,
  },
  {
    dataField: 'date',
    text: 'Date',
    sort: true,
  },
  {
    dataField: 'type',
    text: 'Type',
    sort: true,
  },
  {
    dataField: 'status',
    text: 'Status',
    sort: true,
  },
  {
    dataField: 'priority',
    text: 'Priority',
    sort: true,
  },
  {
    dataField: 'estimatedTime',
    text: 'Estimated Time',
    sort: true,
  },
]

const defaultSorted = [
  {
    dataField: 'name',
    order: 'desc',
  },
]

const expandRow = {
  onlyOneExpanding: true,
  renderer: (row) => (
    <div>
      <p>{`This Expand row is belong to rowKey ${row.id}`}</p>
      <p>
        You can render anything here, also you can add additional data on every
        row object
      </p>
      <p>expandRow.renderer callback will pass the origin row object to you</p>
    </div>
  ),
}
const options = {
  sizePerPageList: [
    {
      text: '5',
      value: 5,
    },
    {
      text: '10',
      value: 10,
    },
    {
      text: 'All',
      value: data.length,
    },
  ], // A numeric array is also available. the purpose of above example is custom the text
}

const Home = (props) => {
  return (
    <React.Fragment>
      <BootstrapTable
        keyField='id'
        data={data}
        columns={columns}
        expandRow={expandRow}
        defaultSorted={defaultSorted}
        pagination={paginationFactory(options)}
      />
    </React.Fragment>
  )
}
export default Home
