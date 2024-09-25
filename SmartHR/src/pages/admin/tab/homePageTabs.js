import {AppBar, Box, Tab, Tabs} from '@mui/material'
import {useState} from 'react'
import HomePageTab from '../homepagesetting/HomePageTab'
import ReasonPageTab from '../homepagesetting/ReasonPageTab'
const tabSettings = [
  {label: 'Home Page Setting', Component: HomePageTab},
  {label: 'Home Page Reason', Component: ReasonPageTab},
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
      {value === index && <Box sx={{py: 0.5}}>{children}</Box>}
    </div>
  )
}

const HomePageTabs = () => {
  const [value, setValue] = useState(0)

  const handleChange = (event, newValue) => {
    setValue(newValue)
  }

  return (
    <Box sx={{width: '100%'}}>
      <AppBar position="static" sx={{bgcolor: '#F4F5FA'}}>
        <Tabs
          value={value}
          onChange={handleChange}
          aria-label="simple tabs example"
          sx={{bgcolor: '343A40 ', color: 'black'}}
        >
          {tabSettings.map((tab, index) => (
            <Tab label={tab.label} key={index} />
          ))}
        </Tabs>
      </AppBar>
      {tabSettings.map((tab, index) => (
        <TabPanel value={value} index={index} key={index}>
          <tab.Component />
        </TabPanel>
      ))}
    </Box>
  )
}

export default HomePageTabs
