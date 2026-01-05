import React, {useState, useEffect} from 'react'
import {Paper} from '@mui/material'
import {Box} from '@mui/system'
import ComponentTable from '../../../../../components/table'
import useGetOkrs from '../../../../../pages/okr/requests/getOkrs'

const columns = [
  {id: 'title', label: 'Title', width: '35%', custom: true, color: '#0070FF'},
  {id: 'type', label: 'Type', width: '10%'},
  {id: 'scope', label: 'Scope', width: '10%'},
  {id: 'cycle', label: 'Cycle', width: '5%'},
  {id: 'departmentName', label: 'Department', width: '10%'},
  {id: 'owner', label: 'Owner', width: '15%'},
  {id: 'parentAlignment', label: 'Parent OKR', width: '15%'},
  {
    id: 'status',
    label: 'Status',
    width: '10%',
    update_status: true,
  },
]

const ListParentAlignment = ({onSelectOneRow}) => {
  const [page, setPage] = useState(0)
  const [rowsPerPage, setRowsPerPage] = useState(10)
  const [openDialog, setOpenDialog] = useState(false)
  const onChangePage = (newPage) => {
    setPage(newPage)
  }
  const departmentId = localStorage.getItem('departmentId')

  const onChangeRowPerPage = (rowsPerPage) => {
    setRowsPerPage(rowsPerPage)
    setPage(0)
  }

  const {data, isLoading, refetch} = useGetOkrs(
    page + 1,
    rowsPerPage,
    departmentId,
  )

  const filteredData = data?.data?.items.filter(
    (item) =>
      (item.type === 'Công việc chung' && item.status === 'Processing') ||
      item.status === 'To Do',
  )
  const formattedData = filteredData?.map((item) => ({
    ...item,
  }))

  const totalItems = filteredData?.length

  useEffect(() => {
    refetch()
  }, [page, rowsPerPage, refetch])

  const onActionSelectRow = (selectedRow) => {
    if (selectedRow) {
      onSelectOneRow(selectedRow)
      setOpenDialog(false)
    }
  }
  return (
    <Paper>
      <Box sx={{width: '100%'}}>
        <Paper sx={{width: '100%'}}>
          <ComponentTable
            columns={columns}
            isLoading={isLoading}
            data={formattedData}
            disableColCheckbox={true}
            onActionSelectRow={onActionSelectRow}
            totalItems={totalItems}
            onChangePage={onChangePage}
            onChangeRowPerPage={onChangeRowPerPage}
            disablePaginationTable={true}
          />
        </Paper>
      </Box>
    </Paper>
  )
}

export default ListParentAlignment
