import React from 'react'
import PropTypes from 'prop-types'
import {
  TableHead,
  TableRow,
  TableCell,
  Checkbox,
  TableSortLabel,
  Box,
} from '@mui/material'
import {visuallyHidden} from '@mui/utils'

function ComponentTableHeader(props) {
  const {
    columns,
    onSelectAllClick,
    order,
    orderBy,
    numSelected,
    rowCount,
    onRequestSort,
    disableColCheckbox,
    disableCheckBox,
  } = props
  const createSortHandler = (property) => (event) => {
    onRequestSort(event, property)
  }

  return (
    <TableHead>
      <TableRow>
        {!disableColCheckbox && (
          <TableCell padding="checkbox">
            <Checkbox
              color="primary"
              indeterminate={numSelected > 0 && numSelected < rowCount}
              checked={rowCount > 0 && numSelected === rowCount}
              disabled={disableCheckBox}
              onChange={onSelectAllClick}
              inputProps={{'aria-label': 'select all desserts'}}
            />
          </TableCell>
        )}
        {columns.map((column) => (
          <TableCell
            key={column.id}
            align={'center'}
            padding={column.disablePadding ? 'none' : 'normal'}
            sortDirection={orderBy === column.id ? order : false}
            sx={{pl: 5}}
          >
            <TableSortLabel
              active={orderBy === column.id}
              direction={orderBy === column.id ? order : 'asc'}
              onClick={createSortHandler(column.id)}
            >
              {column.label}
              {orderBy === column.id ? (
                <Box component="span" sx={visuallyHidden}>
                  {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                </Box>
              ) : null}
            </TableSortLabel>
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  )
}

ComponentTableHeader.propTypes = {
  numSelected: PropTypes.number.isRequired,
  onRequestSort: PropTypes.func.isRequired,
  onSelectAllClick: PropTypes.func.isRequired,
  order: PropTypes.oneOf(['asc', 'desc']).isRequired,
  orderBy: PropTypes.string.isRequired,
  rowCount: PropTypes.number.isRequired,
}

export default ComponentTableHeader
