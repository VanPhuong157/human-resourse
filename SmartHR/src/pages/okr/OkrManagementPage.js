import {
  AppBar,
  Box,
  Button,
  CircularProgress,
  Tab,
  Tabs,
  Typography,
} from '@mui/material'
import {useEffect, useState} from 'react'
import OkrTab from './okrTab/OkrTab'
import RequestTab from './requestTab/RequestTab'
import {styled} from '@mui/material/styles'
import AddIcon from '@mui/icons-material/Add'
import CustomDialog from '../../components/customDialog'
import CreateOKR from './okrTab/create/CreateOKR'
import useGetUserPermissions from '../../pages/admin/requests/getUserPermissions'

const HeaderContainer = styled('div')({
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center',
  marginLeft: '10px',
})

const Title = styled(Typography)({
  color: '#1277b0',
  fontWeight: 500,
  fontSize: '38px',
  lineHeight: '150%',
  fontFamily: 'Inter, sans-serif',
})

const CreateButton = styled(Button)({
  fontFamily: 'Inter, sans-serif',
  borderRadius: '8px',
  boxShadow: '0px 1px 2px 0px rgba(16, 24, 40, 0.05)',
  border: '1px solid rgba(0, 112, 255, 1)',
  backgroundColor: '#0070ff',
  padding: '10px 16px',
  color: '#fff',
  fontWeight: 500,
  fontSize: '14px',
  lineHeight: '143%',
})

const OkrPage = () => {
  const [value, setValue] = useState(0)
  const [openDetailDialog, setOpenDetailDialog] = useState(false)

  return (
    <Box sx={{width: '100%'}}>
      <OkrTab></OkrTab>
    </Box>
  )
}

export default OkrPage
