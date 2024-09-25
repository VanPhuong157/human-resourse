import React, {useEffect, useState} from 'react'
import {
  Button,
  TextField,
  Select,
  MenuItem,
  FormControl,
  Typography,
  Box,
  Slider,
  Grid,
} from '@mui/material'
import {styled} from '@mui/material/styles'
import {baseUrl} from '../../../../api/rootApi'

const StyledFormControl = styled(FormControl)(({theme}) => ({
  margin: theme.spacing(1),
  minWidth: 120,
}))

const StyledSlider = styled(Slider)(({theme}) => ({
  width: '100%',
  marginTop: theme.spacing(2),
  marginBottom: theme.spacing(2),
}))

const StyledSelect = styled(Select)({
  width: '180px',
  height: '55px',
})

const Field = styled(TextField)({
  marginBottom: '25px',
  width: '100%',
})

const StyledTilte = styled(Typography)({
  marginTop: '16px',
  justifyContent: 'center',
})

const FileDownloadButton = styled(Button)(({theme}) => ({
  borderRadius: '54px',
  border: `1px solid ${theme.palette.text.primary}`,
  color: '#12448a',
  fontWeight: 200,
  width: '50%',
  justifyContent: 'flex-start',
  padding: '7px 12px',
  marginTop: '13px',
  marginLeft: '7px',
}))

const OkrRequestDetail = ({okrDetailData}) => {
  const data = okrDetailData?.data
  const [parentAlignment, setParentAlignment] = useState(data?.parentAlignment)
  const [cycle, setCycle] = useState(data?.cycle)
  const [type, setType] = useState(data?.type)
  const [targerNumber, setTargetNumber] = useState(data?.targerNumber)
  const [achieved, setAchieved] = useState(data?.achieved)
  const [scope, setScope] = useState(data?.scope)
  const [unitOfTarget, setUnitTarget] = useState(data?.unitOfTarget)
  const [department, setDepartment] = useState(data?.departmentName)
  const [description, setDescription] = useState(data?.content)
  const [confidentLevel, setConfidentLevel] = useState(data?.confidenceLevel)
  const [expectResult, setExpectResult] = useState(data?.result)
  const [title, setTitle] = useState(data?.title)
  const [reason, setReason] = useState(data?.reason)
  const [actionPlan, setActionPlan] = useState(data?.actionPlan)
  const [reasonLog, setReasonLog] = useState([])
  const [blobUrl, setBlobUrl] = useState('')

  useEffect(() => {
    if (okrDetailData?.data?.actionPlanDetails) {
      fetch(baseUrl + `${okrDetailData.data.actionPlanDetails}`)
        .then((response) => response.blob())
        .then((blob) => {
          const url = URL.createObjectURL(blob)
          setBlobUrl(url)
        })
        .catch((error) => console.error('Error fetching file:', error))
    }
  }, [okrDetailData?.data?.actionPlanDetails])

  useEffect(() => {
    const data = okrDetailData?.data
    if (data) {
      setParentAlignment(data.parentAlignment || 'Empty')
      setCycle(data.cycle)
      setType(data.type)
      setScope(data.scope)
      setTargetNumber(data.targerNumber)
      setAchieved(data.achieved)
      setUnitTarget(data.unitOfTarget)
      setDepartment(data.departmentName)
      setDescription(data.content)
      setConfidentLevel(data.confidenceLevel)
      setExpectResult(data.result || 'Empty')
      setTitle(data.title)
      setReason(data.reason)
      setActionPlan(data.actionPlan)
    }
  }, [okrDetailData])

  const handleReasonChange = (e) => {
    const newReason = e.target.value
    setReason(newReason)

    const newLogEntry = {
      id: reasonLog.length + 1,
      timestamp: new Date().toLocaleString(),
      value: newReason,
    }

    setReasonLog((prevLog) => [...prevLog, newLogEntry])
  }

  const handleFileDownload = () => {
    if (actionPlan && actionPlan.url) {
      const link = document.createElement('a')
      link.href = actionPlan.url
      link.target = '_blank'
      link.setAttribute('download', actionPlan.name || 'file')
      document.body.appendChild(link)
      link.click()
      document.body.removeChild(link)
    }
  }

  return (
    <div>
      <Field
        placeholder="Untitle"
        variant="filled"
        fontSize="30px"
        size="large"
        InputProps={{
          readOnly: true,
        }}
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        sx={{
          '& .MuiInputBase-input::placeholder': {
            fontSize: '35px',
          },
          '& .MuiInputBase-input': {
            fontSize: '35px',
            color: '#1277B0',
          },
        }}
      />
      <Grid container spacing={2}>
        {[
          {
            label: 'Main Result & Content',
            value: description,
            setter: setDescription,
            type: 'text',
          },
          {
            label: 'Target Number',
            value: targerNumber,
            setter: setTargetNumber,
            type: 'text',
          },
          {
            label: 'Achieved',
            value: achieved,
            setter: setAchieved,
            type: 'text',
          },
          {
            label: 'Unit Of Target',
            value: unitOfTarget,
            setter: setUnitTarget,
            type: 'text',
          },
          {
            label: 'Parent Alignment',
            value: parentAlignment,
            setter: setParentAlignment,
            type: 'text',
          },
          {label: 'Cycle', value: cycle, setter: setCycle, type: 'text'},
          {label: 'Type', value: type, setter: setType, type: 'text'},
          {label: 'Scope', value: scope, setter: setScope, type: 'text'},
          {
            label: 'Confident Level',
            value: confidentLevel,
            setter: setConfidentLevel,
            type: 'text',
          },
          {
            label: 'Action Plan',
            value: actionPlan,
            setter: setActionPlan,
            type: 'upload',
          },
          {
            label: 'Expect Result',
            value: expectResult,
            setter: setExpectResult,
            type: 'text',
          },
          {
            label: 'Reason',
            value: reason,
            setter: setReason,
            type: 'log',
          },
          {
            label: 'Department',
            value: department,
            setter: setDepartment,
            type: 'text',
          },
        ].map((item, index) => (
          <React.Fragment key={index}>
            <Grid item xs={3}>
              <StyledTilte sx={{marginLeft: '30px'}} variant="subtitle1">
                {item.label}:
              </StyledTilte>
            </Grid>
            <Grid item xs={9}>
              {item.type === 'log' && (
                <Box>
                  <TextField
                    fullWidth
                    variant="outlined"
                    multiline
                    InputProps={{
                      readOnly: true,
                    }}
                    rows={4}
                    value={item.value}
                    onChange={handleReasonChange}
                    sx={{
                      marginLeft: '7px',
                    }}
                  />
                </Box>
              )}
              {item.type === 'text' && item.label === 'Reason' && (
                <TextField
                  fullWidth
                  variant="outlined"
                  InputProps={{
                    readOnly: true,
                  }}
                  value={reason}
                  onChange={(e) => setReason(e.target.value)}
                  sx={{
                    marginLeft: '7px',
                    ...(item.label === 'Target Number' ||
                    item.label === 'Achieved' ||
                    item.label === 'Unit Of Target'
                      ? {width: '200px', height: '2px', marginBottom: '50px'}
                      : {}),
                  }}
                />
              )}
              {item.type === 'text' && item.label !== 'Reason' && (
                <TextField
                  fullWidth
                  variant="outlined"
                  InputProps={{
                    readOnly: true,
                  }}
                  value={item.value}
                  onChange={(e) => item.setter(e.target.value)}
                  sx={{
                    marginLeft: '7px',
                    ...(item.label === 'Target Number' ||
                    item.label === 'Achieved' ||
                    item.label === 'Unit Of Target'
                      ? {width: '200px', height: '2px', marginBottom: '50px'}
                      : {}),
                  }}
                />
              )}
              {item.type === 'select' && (
                <StyledFormControl fullWidth>
                  <StyledSelect
                    id={item.label.toLowerCase()}
                    value={item.value}
                    onChange={(e) => item.setter(e.target.value)}
                    displayEmpty
                  >
                    <MenuItem value="">
                      <em>Empty</em>
                    </MenuItem>
                    <MenuItem value="option1">Option 1</MenuItem>
                    <MenuItem value="option2">Option 2</MenuItem>
                  </StyledSelect>
                </StyledFormControl>
              )}
              {item.type === 'slider' && (
                <Box sx={{display: 'flex', alignItems: 'center'}}>
                  <Typography
                    variant="body2"
                    color="text.secondary"
                    sx={{mr: 1}}
                  >
                    0
                  </Typography>
                  <StyledSlider
                    value={item.value}
                    onChange={(e, newValue) => item.setter(newValue)}
                    aria-labelledby="processing-slider"
                    valueLabelDisplay="auto"
                    sx={{
                      width: '60%',
                      pointerEvents: 'none',
                    }}
                  />
                  <Typography
                    variant="body2"
                    color="text.secondary"
                    sx={{ml: 1}}
                  >
                    100
                  </Typography>
                </Box>
              )}
              {item.type === 'upload' && (
                <Box sx={{display: 'flex', alignItems: 'center'}}>
                  <Typography
                    variant="body2"
                    color="text.secondary"
                    sx={{mr: 0}}
                  ></Typography>
                  <FileDownloadButton onClick={handleFileDownload}>
                    {actionPlan ? actionPlan : 'Empty'}
                  </FileDownloadButton>
                  <a
                    href={blobUrl}
                    download={actionPlan?.split('/').pop()}
                    style={{
                      marginLeft: '10px',
                      color: 'blue',
                      textDecoration: 'underline',
                      cursor: 'pointer',
                    }}
                  >
                    {' '}
                    Download file
                  </a>
                </Box>
              )}
            </Grid>
          </React.Fragment>
        ))}
      </Grid>
    </div>
  )
}

export default OkrRequestDetail
