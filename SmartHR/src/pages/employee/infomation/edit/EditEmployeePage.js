import {AppBar, Box, Tab, Tabs} from '@mui/material'
import {useState, useEffect} from 'react'
import EmployeeInformationTab from '../EmployeeInformationTab'
import EmployeeFamilyTab from '../EmployeeFamilyTab/EmployeeFamilyTab'
import useGetUserPermissions from '../../../../pages/admin/requests/getUserPermissions'
import EmployeeHistoryTab from '../EmployeeHistoryTab'

const tabSettings = [
  {
    label: 'User Information',
    Component: EmployeeInformationTab,
    requiredPermission: 'EmployeeInformation:Detail',
  },
  {
    label: 'User Family',
    Component: EmployeeFamilyTab,
    requiredPermission: 'EmployeeFamily:List',
  },
  {
    label: 'User History',
    Component: EmployeeHistoryTab,
    requiredPermission: 'EmployeeHistory:List',
  },
]

function TabPanel(props) {
  const {children, value, index, ...other} = props

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{p: 3}}>{children}</Box>}
    </div>
  )
}

const EditEmployeePage = () => {
  const [value, setValue] = useState(0)
  const userId = localStorage.getItem('userId')
  const [permissionData, setPermissionData] = useState([])

  const {data: permissions} = useGetUserPermissions({
    userId,
    pageIndex: 1,
    pageSize: 1000,
  })

  useEffect(() => {
    if (permissions) {
      const newPerm = permissions?.data?.items.map((perm) => perm.name)
      setPermissionData(newPerm)
    }
  }, [permissions])

  const handleChange = (event, newValue) => {
    const selectedTab = filteredTabs[newValue]
    setValue(newValue)
  }

  const filteredTabs = tabSettings.filter(
    (tab) =>
      !tab.requiredPermission ||
      permissionData.includes(tab.requiredPermission),
  )

  return (
    <Box sx={{width: '100%'}}>
      <AppBar position="static" sx={{bgcolor: '#F4F5FA'}}>
        <Tabs
          value={value}
          onChange={handleChange}
          aria-label="simple tabs example"
          sx={{bgcolor: '343A40 ', color: 'black'}}
        >
          {filteredTabs.map((tab, index) => (
            <Tab label={tab.label} key={index} />
          ))}
        </Tabs>
      </AppBar>
      {filteredTabs.map((tab, index) => (
        <TabPanel value={value} index={index} key={index}>
          <tab.Component isVisible={value === index} />
        </TabPanel>
      ))}
    </Box>
  )
}

export default EditEmployeePage
