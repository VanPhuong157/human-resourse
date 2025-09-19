import React, { useMemo, useRef, useState } from 'react'
import { Box, Typography } from '@mui/material'
import { styled } from '@mui/material/styles'
import OkrTab from './okrTab/OkrTab'
import HeaderOkr from './headerOkr/HeaderOkr'
import useGetDepartments from '../../pages/department/requests/getDepartment'

const Title = styled(Typography)({
  color: '#2B2B2B',
  fontWeight: 700,
  fontSize: '38px',
  lineHeight: '150%',
  fontFamily: 'Inter, sans-serif',
  marginBottom: '12px',
  textAlign: 'center',
})

const OkrPage = () => {
  const [activeTab, setActiveTab] = useState(0)
  const [selectedDepartment, setSelectedDepartment] = useState('All Department')
  const { data: departmentsData, isLoading } = useGetDepartments()
  const okrTabRef = useRef(null)

  const tabs = useMemo(
    () => [
      { label: 'All Tasks', filter: {} },
      { label: 'Doing', filter: { status: 'Processing' } },
      { label: 'Done', filter: { status: 'Done' } },
      { label: 'To Do', filter: { status: 'Not Started' } },
      { label: 'PostPone', filter: { status: 'PostPone' } },
      { label: 'Archived', filter: { status: 'Archived' } },
      { label: 'VHS', filter: { company: 'VHS' } },
      { label: 'VNEB', filter: { company: 'VNEB' } },
      { label: 'Quá Hạn', filter: { overdue: true } },
    ],
    [],
  )

  return (
    <Box
      sx={{
        width: '100%',
        minHeight: '100vh',
        display: 'flex',
        flexDirection: 'column',
        px: 3,
        py: 3,
        boxSizing: 'border-box',
      }}
    >
      <Title>All Daily Tasks</Title>

      {/* Header + Tabs + Actions */}
      <HeaderOkr
        isOkr
        activeTab={activeTab}
        onTabChange={setActiveTab}
        tabLabels={tabs.map((t) => t.label)}
        selectedDepartment={selectedDepartment}
        handleDepartmentChange={(e) => setSelectedDepartment(e.target.value)}
        departmentsData={departmentsData}
        isLoading={isLoading}
        handleClickFilter={() => okrTabRef.current?.openFilter()}
        handleClickAdd={() => okrTabRef.current?.openAdd()}
      />

      {/* Vùng bảng chiếm phần còn lại của màn hình */}
      <Box sx={{ flex: 1, minHeight: 0 /* để con có thể overflow */ }}>
        <OkrTab
          ref={okrTabRef}
          selectedDepartment={selectedDepartment}
          quickFilter={tabs[activeTab].filter}
        />
      </Box>
    </Box>
  )
}

export default OkrPage
