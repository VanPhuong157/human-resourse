import React, {useEffect, useRef, useState} from 'react'
import {AgGridReact} from 'ag-grid-react'
import 'ag-grid-enterprise'
import 'ag-grid-community/styles/ag-grid.css'
import 'ag-grid-community/styles/ag-theme-alpine.css'
import CustomDialog from '../../../components/customDialog'
import OkrDetail from './okrDetail/OkrDetail'
import useGetOkrs from '../requests/getOkrs'
import HeaderOkr from '../headerOkr/HeaderOkr'
import {Text} from 'recharts'
import FormDialog from '../../../components/formDialog/FormDialog'
import useGetDepartments from '../../../pages/department/requests/getDepartment'
import OkrFilter from './filter/OkrFilter'
import CreateOKR from './create/CreateOKR'
import {Paper, styled, Typography} from '@mui/material'
import {Box} from '@mui/system'
import {LinearProgress} from '@mui/material'

const CustomLinearProgress = styled(LinearProgress)(({value, theme}) => ({
  height: 10,
  borderRadius: 5,
  backgroundColor: theme.palette.grey[300],
  '& .MuiLinearProgress-bar': {
    backgroundColor:
      value === 0
        ? theme.palette.grey[300]
        : value === 100
          ? theme.palette.success.main
          : theme.palette.info.main,
  },
}))

const OkrTab = () => {
  const [openDetailDialog, setOpenDetailDialog] = useState(false)
  const [okrDetailData, setOkrDetailData] = useState()
  const [selectedDepartment, setSelectedDepartment] = useState('All Department')
  const [openFilterDialog, setOpenFilterDialog] = useState(false)
  const [dataFilterDialog, setDataFilterDialog] = useState({
    title: '',
    type: '',
    scope: '',
    status: '',
    cycle: '',
  })
  const isOkr = true

  const departmentId = localStorage.getItem('departmentId')
  const {data, refetch, isLoading} = useGetOkrs(
    1,
    100,
    departmentId,
    dataFilterDialog,
  )
  const [openAddDetailDialog, setOpenAddDetailDialog] = useState(false)
  const handleAddDetailDialogCancel = () => {
    setOpenAddDetailDialog(false)
  }

  const handleAddDetailDialogOpen = () => {
    setOpenAddDetailDialog(true)
  }
  const handleAddDetailDialogSubmit = () => {
    setOpenAddDetailDialog(false)
    refetch()
  }
  useEffect(() => {
    refetch()
  }, [selectedDepartment, dataFilterDialog, refetch])

  const handleNameClick = (event) => {
    setOkrDetailData(event.data)
    setOpenDetailDialog(true)
  }

  const handleDetailDialogCancel = () => {
    setOpenDetailDialog(false)
    refetch()
  }

  const handleConfirmFilterDialog = (formData) => {
    setDataFilterDialog(formData)
    setOpenFilterDialog(false)
  }

  const handleDepartmentChange = (event) => {
    setSelectedDepartment(event.target.value)
  }

  const handleClickFilter = () => {
    setOpenFilterDialog(true)
    refetch()
  }

  const handleCancelFilterDialog = () => {
    setOpenFilterDialog(false)
  }

  const autoGroupColumnDef = {
    headerName: 'Title',
    minWidth: 500,
    cellRenderer: 'agGroupCellRenderer',
    suppressHeaderMenuButton: true,
    cellRendererParams: {
      innerRenderer: ({data}) => (
        <Text style={{color: '#186BFF'}}>{data.title}</Text>
      ),
      suppressCount: true,
    },
  }

  const columnDefs = [
    {
      field: 'type',
      headerName: 'Type',
      suppressHeaderMenuButton: true,
      cellRenderer: ({value}) => (
        <Typography
          sx={{
            justifyContent: 'center',
            alignItems: 'center',
            marginTop: '10px',
          }}
          variant="body2"
          style={{
            color:
              value === 'Objective'
                ? '#4caf50'
                : value === 'KeyResult'
                  ? '#9c27b0'
                  : 'inherit',
          }}
        >
          {value}
        </Typography>
      ),
    },
    {field: 'scope', headerName: 'Scope', suppressHeaderMenuButton: true},
    {field: 'owner', headerName: 'Owner', suppressHeaderMenuButton: true},
    {
      field: 'departmentName',
      headerName: 'Department',
      suppressHeaderMenuButton: true,
    },
    {field: 'cycle', headerName: 'Cycle', suppressHeaderMenuButton: true},
    {
      field: 'progress',
      headerName: 'Progress',
      suppressHeaderMenuButton: true,
      cellRenderer: ({value}) => (
        <Box sx={{display: 'flex', alignItems: 'center', marginTop: '10px'}}>
          <Box sx={{width: '100%', mr: 1}}>
            <CustomLinearProgress
              variant="determinate"
              value={Math.min(Math.max(value, 0), 100)} // Đảm bảo giá trị nằm trong khoảng 0-100
            />
          </Box>
          <Box sx={{minWidth: 35}}>
            <Typography variant="body2" color="textSecondary">{`${Math.round(
              value,
            )}%`}</Typography>
          </Box>
        </Box>
      ),
    },
    {
      field: 'status',
      headerName: 'Status',
      suppressHeaderMenuButton: true,
      cellRenderer: ({value}) => {
        let color = ''
        if (value === 'Processing') {
          color = '#FFC107' // Yellow for Processing
        } else if (value === 'Done') {
          color = '#4CAF50' // Green for Done
        } else {
          color = '#000000' // Default black for other statuses
        }

        return (
          <Typography
            sx={{
              justifyContent: 'center',
              alignItems: 'center',
              marginTop: '10px',
            }}
            variant="body2"
            style={{
              color: color,
            }}
          >
            {value}
          </Typography>
        )
      },
    },
  ]

  const defaultColDef = {
    flex: 1,
    sortable: false,
    filter: false,
  }

  const getDataPath = ({id, parentId}) => {
    const result = [id]
    let row = data?.data?.items.find((row) => row.id === parentId)
    while (row) {
      result.unshift(row.id)
      // eslint-disable-next-line no-loop-func
      row = data?.data?.items.find((r) => r.id === row.parentId)
    }
    return result
  }

  const filteredData = data?.data?.items.filter(
    (item) =>
      selectedDepartment === 'All Department' ||
      item.departmentId === selectedDepartment,
  )

  const gridRef = useRef()

  return (
    <Paper>
      <Box sx={{width: '100%'}}>
        <HeaderOkr
          isOkr={isOkr}
          selectedDepartment={selectedDepartment}
          handleDepartmentChange={handleDepartmentChange}
          departmentsData={useGetDepartments().data}
          isLoading={isLoading}
          handleClickFilter={handleClickFilter}
          handleClickAdd={handleAddDetailDialogOpen}
        />
        <div
          className="ag-theme-alpine"
          style={{width: '100%', height: '700px'}}
        >
          <AgGridReact
            rowData={filteredData}
            treeData={true}
            animateRows={true}
            autoGroupColumnDef={autoGroupColumnDef}
            columnDefs={columnDefs}
            defaultColDef={defaultColDef}
            getDataPath={getDataPath}
            ref={gridRef}
            pagination={false}
            paginationPageSize={2}
            onRowClicked={handleNameClick}
          />
        </div>
        <CustomDialog
          open={openDetailDialog}
          onCancel={handleDetailDialogCancel}
          title="Okr Detail"
          viewDialog
          showCloseButton
          maxWidth="md"
          dialogContent={<OkrDetail data={okrDetailData} />}
        />
        <FormDialog
          open={openFilterDialog}
          onCancel={handleCancelFilterDialog}
          onConfirm={handleConfirmFilterDialog}
          title="Filter OKR Request"
          actionName="Filter"
          isLoading={isLoading}
          dialogContent={<OkrFilter data={dataFilterDialog} />}
        />
        <CustomDialog
          open={openAddDetailDialog}
          onCancel={handleAddDetailDialogCancel}
          title="Add OKR"
          viewDialog
          showCloseButton
          maxWidth="lg"
          actionName="Submit"
          dialogContent={
            <CreateOKR onClose={() => handleAddDetailDialogSubmit()} />
          }
        />
      </Box>
    </Paper>
  )
}

export default OkrTab
