import React from 'react'
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Checkbox,
  Box,
  Typography,
  MenuItem,
  Link,
  Menu,
  Button,
} from '@mui/material'
import {format, parse} from 'date-fns'
import MoreVertIcon from '@mui/icons-material/MoreVert'
import {getComparator, stableSort} from '../utils'
import ComponentTableHeader from './HeaderTable'
import PaginationTable from '../../components/pagination/Pagination'
import eventBus, {EVENT_BUS_KEY} from '../../services/eventBus'
import {Tooltip} from '@mui/material'

const ComponentTable = ({
  columns,
  data,
  dataSelected,
  totalItems,
  isLoading,
  initialData,
  fetchData,
  disablePaginationTable,
  onClickOpenDialog,
  disableColCheckbox,
  disableCheckBox,
  displaySaveButton,
  onChangePage,
  onChangeRowPerPage,
  onActionViewDetail,
  onActionUpdateStatus,
  onActionEdit,
  onActionNavigate,
  onActionSelectRow,
  onActionDelete,
  onActionUpdatePosition,
  onSave,
  hideDeleteForBasicType,
  hideEdit,
}) => {
  const [page, setPage] = React.useState(0)
  const [rowsPerPage, setRowsPerPage] = React.useState(10)
  const [order, setOrder] = React.useState('asc')
  const [orderBy, setOrderBy] = React.useState(columns[0]?.id || '')
  const [selected, setSelected] = React.useState([])
  const [anchorEl, setAnchorEl] = React.useState(null)
  const [dataUpdate, setDataUpdate] = React.useState({})

  React.useEffect(() => {
    const selectedIds = dataSelected?.map((item) => item.id)
    setSelected(selectedIds || [])
    eventBus.emit(EVENT_BUS_KEY.SELECTED_COLUMNS, selectedIds || [])
  }, [dataSelected, onChangePage, onChangeRowPerPage])

  const handleChangePage = (event, newPage) => {
    setPage(newPage)
    onChangePage(newPage)
  }

  const handleChangeRowsPerPage = (event) => {
    const rows = parseInt(event.target.value, 10)
    setRowsPerPage(rows)
    onChangeRowPerPage(rows)
    setPage(0)
  }

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === 'asc'
    setOrder(isAsc ? 'desc' : 'asc')
    setOrderBy(property)
  }

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelected = data.map((n) => n.id)
      setSelected(newSelected)
      eventBus.emit(EVENT_BUS_KEY.SELECTED_COLUMNS, newSelected)
      return
    }
    eventBus.emit(EVENT_BUS_KEY.SELECTED_COLUMNS, [])
    setSelected([])
  }

  const handleClick = (event, id) => {
    const selectedIndex = selected?.indexOf(id)
    let newSelected = []
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, id)
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1))
    } else if (selectedIndex === selected?.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1))
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1),
      )
    }
    setSelected(newSelected)
    eventBus.emit(EVENT_BUS_KEY.SELECTED_COLUMNS, newSelected)
    if (disableColCheckbox) {
      onActionSelectRow(newSelected[0])
    }
  }

  const isSelected = (id) => selected?.indexOf(id) !== -1

  const visibleRows = React.useMemo(() => {
    if (isLoading || !data) return []
    return stableSort(data, getComparator(order, orderBy)).slice(0, rowsPerPage)
  }, [order, orderBy, rowsPerPage, isLoading, data])

  const handleMoreVertClick = (event, row) => {
    setAnchorEl(event.currentTarget)
    setDataUpdate(row)
  }

  const handleMenuClose = () => {
    setAnchorEl(null)
  }

  const handleSave = () => {
    if (onSave) {
      onSave(selected)
    }
  }

  const shouldHideDelete = (row) => {
    return hideDeleteForBasicType && row.type === 'Basic'
  }

  const userId = localStorage.getItem('userId')
  const shouldEdit = (row) => {
    return (
      hideEdit && (row.approveStatus === 'Approve' || userId !== row.ownerId)
    )
  }

  return (
    <>
      {displaySaveButton && (
        <Button variant="contained" onClick={handleSave}>
          Save
        </Button>
      )}
      <TableContainer>
        <Table
          sx={{minWidth: 750}}
          aria-labelledby="tableTitle"
          size={'medium'}
        >
          <ComponentTableHeader
            columns={columns}
            numSelected={selected?.length}
            order={order}
            orderBy={orderBy}
            onSelectAllClick={handleSelectAllClick}
            onRequestSort={handleRequestSort}
            rowCount={data?.length}
            disableColCheckbox={disableColCheckbox}
            disableCheckBox={disableCheckBox}
          />
          <TableBody>
            {data?.length !== 0 &&
              !isLoading &&
              visibleRows.map((row, index) => {
                const isItemSelected = isSelected(row.id)
                const labelId = `enhanced-table-checkbox-${index}`
                return (
                  <TableRow
                    hover
                    onClick={(event) => {
                      if (!disableColCheckbox && !disableCheckBox)
                        handleClick(event, row.id)
                      if (
                        disableColCheckbox &&
                        onActionSelectRow &&
                        !disableCheckBox
                      ) {
                        handleClick(event, row)
                      }
                    }}
                    role="checkbox"
                    aria-checked={isItemSelected}
                    tabIndex={-1}
                    key={row.id}
                    selected={isItemSelected}
                    sx={{cursor: 'pointer'}}
                  >
                    {!disableColCheckbox && (
                      <TableCell padding="checkbox">
                        <Checkbox
                          color="primary"
                          checked={isItemSelected}
                          disabled={disableCheckBox}
                          inputProps={{'aria-labelledby': labelId}}
                        />
                      </TableCell>
                    )}
                    {columns.map((column) => {
                      let value = row[column.id] // Đảm bảo giá trị không bao giờ là undefined
                      // let value =
                      //   row[column.id] !== undefined && row[column.id] !== null
                      //     ? row[column.id]
                      //     : ''
                      
                      if (column.id === 'action') {
                        return (
                          <TableCell
                            key={column.id}
                            align="center"
                            padding="none"
                            sx={{width: column.width, maxWidth: column.width}}
                          >
                            <MoreVertIcon
                              onClick={(event) =>
                                handleMoreVertClick(event, row)
                              }
                            />
                            <Menu
                              anchorEl={anchorEl}
                              open={Boolean(anchorEl)}
                              onClose={handleMenuClose}
                            >
                              {column.update_status && (
                                <MenuItem
                                  key="update-status"
                                  onClick={() => {
                                    handleMenuClose()
                                    onActionUpdateStatus(dataUpdate)
                                  }}
                                >
                                  Update Status
                                </MenuItem>
                              )}
                              {column.update_position && (
                                <MenuItem
                                  key="update-position"
                                  onClick={() => {
                                    handleMenuClose()
                                    onActionUpdatePosition(dataUpdate)
                                  }}
                                >
                                  Update Position
                                </MenuItem>
                              )}
                              {column.edit && !shouldEdit(dataUpdate) && (
                                <MenuItem
                                  key="edit"
                                  onClick={() => {
                                    handleMenuClose()
                                    onActionEdit(dataUpdate)
                                  }}
                                >
                                  Edit
                                </MenuItem>
                              )}
                              {column.delete &&
                                !shouldHideDelete(dataUpdate) && (
                                  <MenuItem
                                    key="delete"
                                    onClick={() => {
                                      handleMenuClose()
                                      onActionDelete(dataUpdate)
                                    }}
                                  >
                                    Delete
                                  </MenuItem>
                                )}
                              {column.view_detail && (
                                <MenuItem
                                  key="view-detail"
                                  onClick={() => {
                                    handleMenuClose()
                                    onActionViewDetail(dataUpdate)
                                  }}
                                >
                                  View Detail
                                </MenuItem>
                              )}
                            </Menu>
                          </TableCell>
                        )
                      }
                      if (column.id === 'approveStatus') {
                        let cellStyle = {}
                        // Kiểm tra giá trị của status để đổi màu
                        if (value === 'Approve') {
                          cellStyle.color = 'green' // Màu xanh lá cho trạng thái Approve
                        } else if (value === 'Reject') {
                          cellStyle.color = 'red' // Màu đỏ cho trạng thái Reject
                        } else {
                          cellStyle.color = 'gray' // Màu xám cho các trạng thái khác
                        }

                        return (
                          <TableCell
                            key={column.id}
                            align={'center'}
                            sx={{
                              width: column.width,
                              maxWidth: column.width,
                              whiteSpace: 'normal',
                              ...cellStyle,
                            }}
                          >
                            {value}
                          </TableCell>
                        )
                      }
                      if (column.id === 'type') {
                        let cellStyle = {}
                        // Kiểm tra giá trị của status để đổi màu
                        if (value === 'Objective') {
                          cellStyle.color = 'green' // Màu xanh lá cho trạng thái Approve
                        } else if (value === 'KeyResult') {
                          cellStyle.color = 'purple' // Màu đỏ cho trạng thái Reject
                        } else {
                          cellStyle.color = 'gray' // Màu xám cho các trạng thái khác
                        }

                        return (
                          <TableCell
                            key={column.id}
                            align={'center'}
                            sx={{
                              width: column.width,
                              maxWidth: column.width,
                              whiteSpace: 'normal',
                              ...cellStyle,
                            }}
                          >
                            {value}
                          </TableCell>
                        )
                      }
                      // if (column.dialog) {
                      //   return (
                      //     <TableCell
                      //       key={column.id}
                      //       align={'center'}
                      //       sx={{
                      //         width: column.width,
                      //         maxWidth: column.width,
                      //         whiteSpace: 'normal',
                      //         ...cellStyle,
                      //       }}
                      //     >
                      //       {value}
                      //     </TableCell>
                      //   )
                      // }
                      if (column.dialog) {
                        return (
                          <TableCell
                            key={column.id}
                            align={'center'}
                            sx={{
                              width: column.width,
                              maxWidth: 200,
                              whiteSpace: 'nowrap', // Không xuống dòng
                              overflow: 'hidden', // Ẩn phần văn bản vượt quá
                              textOverflow: 'ellipsis', // Hiển thị dấu ba chấm nếu văn bản quá dài
                              wordWrap: 'break-word', // Chia từ nếu từ quá dài
                            }}
                          >
                            <Link
                              component="button"
                              variant="body2"
                              sx={{textDecoration: 'None'}}
                              onClick={(e) => {
                                e.stopPropagation()
                                onClickOpenDialog(row.id)
                              }}
                            >
                              {value}
                            </Link>
                          </TableCell>
                        )
                      }
                      let cellStyle = {}
                      if (column.id === 'status' && column.statusPost) {
                        cellStyle = {
                          color: value === 'Recruiting' ? 'green' : 'red',
                        }
                      }
                      if (column.custom) {
                        cellStyle = {color: column.color}
                        return (
                          <TableCell
                            key={column.id}
                            align={'center'}
                            sx={{
                              width: column.width,
                              color: column.color,
                              maxWidth: column.width,
                              whiteSpace: 'normal',
                              ...cellStyle,
                            }}
                          >
                            {value}
                          </TableCell>
                        )
                      }
                      if (column.formatDatetime && value) {
                        const parsedDate = parse(
                          value,
                          'dd/MM/yyyy',
                          new Date(),
                        )
                        value = format(parsedDate, column.formatDatetime)
                      }
                      return (
                        <TableCell
                          key={column.id}
                          align={'center'}
                          sx={{
                            ...cellStyle,
                            width: column.width,
                            maxWidth: 100,
                            fontWeight: column.navigate ? 'bold' : 'normal',
                            cursor: column.navigate ? 'pointer' : 'default',
                            color: column.navigate ? 'blue' : 'inherit',
                            whiteSpace: 'nowrap', // Không xuống dòng
                            overflow: 'hidden', // Ẩn phần văn bản vượt quá
                            textOverflow: 'ellipsis', // Hiển thị dấu ba chấm nếu văn bản quá dài
                            wordWrap: 'break-word', // Chia từ nếu từ quá dài
                          }}
                          onClick={() => {
                            if (column.navigate) {
                              onActionNavigate(row)
                            }
                          }}
                        >
                          <Tooltip title={value} arrow>
                            {value !== undefined && value !== null
                              ? value
                              : column.replaceNoneValue}
                          </Tooltip>
                        </TableCell>
                      )
                    })}
                  </TableRow>
                )
              })}
            {data?.length === 0 && (
              <TableRow>
                <TableCell colSpan={columns.length + 1}>
                  <Box
                    justifyContent="space-round"
                    alignContent="center"
                    textAlign="center"
                    pl={2}
                  >
                    <Typography color="textSecondary">Nothing found</Typography>
                  </Box>
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
      <PaginationTable
        count={totalItems}
        page={page}
        rowsPerPage={rowsPerPage}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
        disablePaginationTable={disablePaginationTable}
      />
    </>
  )
}

export default ComponentTable
