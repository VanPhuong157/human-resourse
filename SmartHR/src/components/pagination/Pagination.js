import React from 'react'
import {TablePagination} from '@mui/material'

function PaginationTable({
  count,
  page,
  rowsPerPage,
  onPageChange,
  onRowsPerPageChange,
  disablePaginationTable,
}) {
  return count !== 0
    ? !disablePaginationTable && (
        <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={count}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={onPageChange}
          onRowsPerPageChange={onRowsPerPageChange}
        />
      )
    : null
}

export default PaginationTable
