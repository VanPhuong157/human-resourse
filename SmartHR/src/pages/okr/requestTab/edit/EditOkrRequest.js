import React, {useState, useEffect} from 'react'
import {
  DialogActions,
  Button,
  TextField,
  Select,
  MenuItem,
  FormControl,
  Typography,
  Box,
  Grid,
  FormHelperText,
} from '@mui/material'
import {styled} from '@mui/material/styles'
import {rootApi} from '../../../../api/rootApi'
import path from '../../../../api/path'
import {showError, showSuccess} from '../../../../components/notification'
import CustomDialog from '../../../../components/customDialog'
import ListParentAlignment from '../../okrTab/create/dialogParent/getParentAligment'
import {StatusCodes} from 'http-status-codes'

const StyledButton = styled(Button)(({theme}) => ({
  margin: theme.spacing(1),
}))

const StyledFormControl = styled(FormControl)(({theme}) => ({
  margin: theme.spacing(1),
  minWidth: 120,
}))

const StyledSelect = styled(Select)({
  width: '180px',
  height: '55px',
})

const Field = styled(TextField)({
  marginBottom: '25px',
  width: '100%',
})

const StyledTitle = styled(Typography)(({theme, required}) => ({
  marginTop: '16px',
  justifyContent: 'center',
  display: 'flex',
  alignItems: 'center',
  '&::after': {
    content: required ? '"*"' : '""',
    color: 'red',
    marginLeft: '4px',
    fontSize: '1rem',
  },
}))

const FileUploadButton = styled(Button)(({theme}) => ({
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

const shakeAnimation = `
  @keyframes shake {
    0% { transform: translateX(0); }
    20% { transform: translateX(-10px); }
    40% { transform: translateX(10px); }
    60% { transform: translateX(-4px); }
    80% { transform: translateX(4px); }
    100% { transform: translateX(0); }
  }
`

const EditOkrRequest = ({
  okrDetailData,
  refetch,
  refetchOkrDetail,
  onClose,
}) => {
  const [initialized, setInitialized] = useState(false)
  const [initialData, setInitialData] = useState(null)
  const [parentAlignment, setParentAlignment] = useState('')
  const [cycle, setCycle] = useState('')
  const [type, setType] = useState('')
  const [scope, setScope] = useState('')
  const [targetNumber, setTargetNumber] = useState('')
  const [achieved, setAchieved] = useState('')
  const [unitTarget, setUnitTarget] = useState('')
  const [department, setDepartment] = useState('')
  const [departmentId, setDepartmentId] = useState('')
  const [description, setDescription] = useState('')
  const [confidentLevel, setConfidentLevel] = useState('')
  const [result, setResult] = useState('')
  const [title, setTitle] = useState('')
  const [actionPlan, setActionPlan] = useState(null)
  const [formErrors, setFormErrors] = useState({})

  useEffect(() => {
    if (!initialized && okrDetailData?.data) {
      const data = okrDetailData.data
      setParentAlignment(data.parentAlignment)
      setCycle(data.cycle)
      setType(data.type)
      setScope(data.scope)
      setTargetNumber(data.targerNumber)
      setAchieved(data.achieved)
      setUnitTarget(data.unitOfTarget)
      setDepartment(data.departmentName)
      setDepartmentId(data.departmentId)
      setDescription(data.content)
      setConfidentLevel(data.confidenceLevel)
      setResult(data.result || 'Empty')
      setTitle(data.title)
      setActionPlan(data.actionPlan)

      setInitialData({
        parentAlignment: data.parentAlignment,
        cycle: data.cycle,
        type: data.type,
        scope: data.scope,
        targetNumber: data.targerNumber,
        achieved: data.achieved,
        unitTarget: data.unitOfTarget,
        department: data.departmentName,
        departmentId: data.departmentId,
        description: data.content,
        confidentLevel: data.confidenceLevel,
        result: data.result || 'Empty',
        title: data.title,
        actionPlan: data.actionPlan,
      })

      setInitialized(true) // Mark as initialized to avoid re-running this effect
    }
  }, [okrDetailData, initialized])

  const [errors, setErrors] = useState({
    title: '',
    description: '',
    targetNumber: '',
    achieved: '',
    unitTarget: '',
    type: '',
    cycle: '',
    scope: '',
    department: '',
    confidentLevel: '',
    expectedResult: '',
  })
  const [openParentAlignmentDialog, setOpenParentAlignmentDialog] =
    useState(false)

  const handleOpenParentAlignmentDialog = () => {
    setOpenParentAlignmentDialog(true)
  }

  const handleCloseParentAlignmentDialog = () => {
    setOpenParentAlignmentDialog(false)
  }

  const onSelectOneRow = (parentAlignment) => {
    if (parentAlignment) {
      setParentAlignment(parentAlignment)
      handleConfirmParent()
    }
  }

  const handleConfirmParent = () => {
    handleCloseParentAlignmentDialog()
  }

  useEffect(() => {
    if (formErrors) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        ...formErrors,
      }))
    }
  }, [formErrors])

  useEffect(() => {
    if (parentAlignment) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        type: '',
      }))
    }
  }, [parentAlignment])

  const validateField = (name, value) => {
    let error = ''
    switch (name) {
      case 'title':
        if (!value) {
          error = 'Title is required'
        } else if (value.length > 200) {
          error = 'Title cannot exceed 200 characters'
        }
        break
      case 'description':
        if (!value) error = 'Main Result & Content is required'
        else if (value.length > 2000)
          error = 'Main Result & Content cannot exceed 2000 characters'
        break
      case 'targetNumber':
        if (!/^\d+$/.test(value) || parseFloat(value) <= 0) {
          error = 'Target Number must be an integer greater than 0'
        } else if (parseFloat(value) > 1000000000) {
          error = 'Target Number cannot exceed 1,000,000,000'
        } else if (achieved && parseFloat(value) < parseFloat(achieved)) {
          error = 'Target Number must be greater than or equal to Achieved'
        }
        break
      case 'achieved':
        if (!/^\d+$/.test(value) || parseFloat(value) < 0) {
          error = 'Achieved must be an integer greater than or equal to 0'
        } else if (parseFloat(value) > 1000000000) {
          error = 'Achieved cannot exceed 1,000,000,000'
        } else if (
          targetNumber &&
          parseFloat(value) > parseFloat(targetNumber)
        ) {
          error = 'Achieved must be less than or equal to Target Number'
        }
        break
      case 'unitTarget':
        if (!value) error = 'Unit of Target is required'
        else if (value.length > 10)
          error = 'Unit of Target cannot exceed 10 characters'
        break
      case 'expectedResult':
        if (value.length > 2000)
          error = 'Expected Result cannot exceed 2000 characters'
        break
      default:
        break
    }
    return error
  }

  const handleInputChange = (e) => {
    const {name, value} = e.target
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: validateField(name, value),
    }))

    // Update state for each field dynamically
    switch (name) {
      case 'title':
        if (value.length <= 200) setTitle(value)
        break
      case 'description':
        if (value.length <= 2000) setDescription(value)
        break
      case 'unitTarget':
        if (value.length <= 10) setUnitTarget(value)
        break
      case 'targetNumber':
        setTargetNumber(value)
        break
      case 'achieved':
        setAchieved(value)
        break
      case 'expectedResult':
        if (value.length <= 2000) setResult(value)
        break
      default:
        break
    }
  }
  const requiredFields = [
    'title',
    'description',
    'targetNumber',
    'achieved',
    'unitTarget',
    'type',
    'cycle',
    'scope',
    'department',
    'confidentLevel',
  ]

  const handleDepartChange = (event) => {
    const {value} = event.target
    setDepartment(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      department: value ? '' : 'Department is required',
    }))
  }
  const handleTypeChange = (event) => {
    const {value} = event.target
    setType(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      type: value ? '' : 'Type is required',
    }))

    if (value === 'KeyResult' && !parentAlignment) {
      setErrors((prevErrors) => ({
        ...prevErrors,
        type: 'Must select Parent Alignment of OKR to select KeyResult',
      }))
    } else {
      setErrors((prevErrors) => ({
        ...prevErrors,
        type: '',
      }))
    }

    setType(value)
  }

  const handleCycleChange = (event) => {
    const {value} = event.target
    setCycle(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      cycle: value ? '' : 'Cycle is required',
    }))
  }

  const handleScopeChange = (event) => {
    const {value} = event.target
    setScope(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      scope: value ? '' : 'Scope is required',
    }))
  }

  const handleConfidenceChange = (event) => {
    const {value} = event.target
    setConfidentLevel(value)
    setErrors((prevErrors) => ({
      ...prevErrors,
      confidentLevel: value ? '' : 'Confident Level is required',
    }))
  }

  const handleEditOkrRequest = async () => {
    const errors = {}

    const currentData = {
      parentAlignment,
      cycle,
      type,
      scope,
      targetNumber,
      achieved,
      unitTarget,
      department,
      departmentId,
      description,
      confidentLevel,
      result,
      title,
      actionPlan,
    }

    const isDataChanged = Object.keys(currentData).some(
      (key) => currentData[key] !== initialData[key],
    )

    if (!isDataChanged) {
      showError({message: 'There are no changes to Edit!'})
      return
    }

    requiredFields.forEach((field) => {
      const value = eval(field) // Access the state dynamically
      if (value === null || value === undefined || value === '') {
        errors[field] =
          `${field.charAt(0).toUpperCase() + field.slice(1)} is required`
      }
    })

    if (type === 'KeyResult' && !parentAlignment) {
      errors.type = 'Must select Parent Alignment of OKR to select KeyResult'
    }

    if (Object.keys(errors).length > 0) {
      setFormErrors(errors)
      return
    }
    const formData = new FormData()

    formData.append('Title', title)
    formData.append('Content', description)
    formData.append('TargetNumber', targetNumber)
    formData.append('Achieved', achieved)
    formData.append('UnitOfTarget', unitTarget)
    formData.append(
      'ParentId',
      parentAlignment?.id || okrDetailData?.data?.parentId || '',
    )
    formData.append('Type', type)
    formData.append('Cycle', cycle)
    formData.append('Scope', scope)
    formData.append('DepartmentId', departmentId)
    formData.append('ConfidenceLevel', confidentLevel)
    formData.append('Result', result || 'Empty')
    if (actionPlan) {
      formData.append('ActionPlan', actionPlan)
    }

    try {
      const response = await rootApi.put(
        path.okr.editOkrRequest({okrId: okrDetailData?.data?.id}),
        formData,
        {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        },
      )
      if (response.data?.code === StatusCodes.OK) {
        showSuccess({message: response.data?.message})
        refetch()
        refetchOkrDetail()
        onClose()
      } else if (response.data?.code === StatusCodes.BAD_REQUEST) {
        showError({message: response.data?.message})
      }
    } catch (err) {
      showError(
        err.response?.data?.message || 'Please fix the error to Edit OKR',
      )
    }
  }

  const handleFileUpload = (e) => {
    const file = e.target.files[0]
    const maxSize = 10 * 1024 * 1024 // 10 MB
    const validFileType = 'application/pdf'

    if (file) {
      if (file.size > maxSize) {
        setErrors((prevErrors) => ({
          ...prevErrors,
          actionPlan: 'The file size must not exceed 10 MB.',
        }))
        setActionPlan(null)
      } else if (file.type !== validFileType) {
        setErrors((prevErrors) => ({
          ...prevErrors,
          actionPlan: 'Only PDF files are allowed.',
        }))
        setActionPlan(null)
      } else {
        setActionPlan(file)
        setErrors((prevErrors) => ({
          ...prevErrors,
          actionPlan: '',
        }))
      }
    }
  }

  const getQuarters = () => {
    const currentMonth = new Date().getMonth() + 1
    const currentYear = new Date().getFullYear()
    const currentQuarter = Math.ceil(currentMonth / 3)
    const quarters = []
    for (let i = 0; i < 4; i++) {
      let quarter = currentQuarter + i
      let year = currentYear
      if (quarter > 4) {
        quarter = quarter - 4
        year = currentYear + 1
      }
      quarters.push({
        label: `Q${quarter} ${year}`,
        value: `Q${quarter} ${year}`,
      })
    }
    return quarters
  }

  return (
    <div>
      <Field
        required
        name="title"
        placeholder="Untitled"
        variant="filled"
        fontSize="30px"
        size="large"
        value={title}
        error={!!errors.title}
        helperText={errors.title}
        onChange={((e) => setTitle(e.target.value), handleInputChange)}
        sx={{
          '& .MuiInputBase-input::placeholder': {
            fontSize: '35px',
          },
          '& .MuiInputBase-input': {
            fontSize: '35px',
            color: '#1277B0',
            ...(errors.title && {
              borderColor: 'red',
              animation: 'shake 0.5s',
            }),
          },
        }}
      />
      <style>{shakeAnimation}</style>
      <Grid container spacing={2}>
        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Main Result & Content:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <TextField
            fullWidth
            variant="outlined"
            name="description"
            value={description}
            error={!!errors.description}
            helperText={errors.description}
            onChange={
              ((e) => setDescription(e.target.value), handleInputChange)
            }
            sx={{
              marginTop: '15px',
              marginLeft: '7px',
              animation: errors.description ? 'shake 0.3s' : 'none',
            }}
          />
        </Grid>

        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Target Number:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <TextField
            fullWidth
            name="targetNumber"
            variant="outlined"
            value={targetNumber}
            error={!!errors.targetNumber}
            helperText={errors.targetNumber}
            onChange={
              ((e) => setTargetNumber(e.target.value), handleInputChange)
            }
            sx={{
              width: '190px',
              marginTop: '15px',
              marginLeft: '7px',
              animation: errors.targetNumber ? 'shake 0.3s' : 'none',
            }}
          />
        </Grid>

        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Achieved:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <TextField
            fullWidth
            name="achieved"
            variant="outlined"
            value={achieved}
            error={!!errors.achieved}
            helperText={errors.achieved}
            onChange={((e) => setAchieved(e.target.value), handleInputChange)}
            sx={{
              width: '190px',
              marginTop: '15px',
              marginLeft: '7px',
              animation: errors.Achieved ? 'shake 0.3s' : 'none',
            }}
          />
        </Grid>

        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Unit of Target:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <TextField
            fullWidth
            name="unitTarget"
            variant="outlined"
            value={unitTarget}
            error={!!errors.unitTarget}
            helperText={errors.unitTarget}
            onChange={((e) => setUnitTarget(e.target.value), handleInputChange)}
            sx={{
              width: '190px',
              marginTop: '15px',
              marginLeft: '7px',
              animation: errors.UnitOfTarget ? 'shake 0.3s' : 'none',
            }}
          />
        </Grid>

        <Grid item xs={3}>
          <StyledTitle sx={{marginLeft: '30px'}} variant="subtitle1">
            Parent Alignment:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <TextField
            fullWidth
            variant="outlined"
            placeholder="Click to select a Parent Alingment"
            value={
              typeof parentAlignment === 'object' && parentAlignment !== null
                ? parentAlignment.title
                : parentAlignment
            }
            onChange={(e) => setParentAlignment(e.target.value)}
            sx={{
              width: '400px',
              marginTop: '15px',
              marginLeft: '7px',
              animation: errors.parentAligment ? 'shake 0.3s' : 'none',
              '& .MuiOutlinedInput-root': {
                color: '#186BFF',
                ...(errors.parentAligment && {
                  borderColor: 'red',
                }),
              },
            }}
            onClick={handleOpenParentAlignmentDialog}
            InputProps={{
              readOnly: true,
            }}
          />
        </Grid>

        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Type:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <StyledFormControl>
            <StyledSelect
              value={type}
              error={!!errors.type}
              helperText={errors.type}
              onChange={((e) => setType(e.target.value), handleTypeChange)}
              displayEmpty
            >
              <MenuItem value=""></MenuItem>
              <MenuItem value={'Objective'}>Objective</MenuItem>
              <MenuItem value={'KeyResult'}>KeyResult</MenuItem>
            </StyledSelect>
            {errors.type && (
              <FormHelperText error>{errors.type}</FormHelperText>
            )}
          </StyledFormControl>
        </Grid>

        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Cycle:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <StyledFormControl>
            <StyledSelect
              value={cycle}
              error={!!errors.cycle}
              helperText={errors.cycle}
              onChange={((e) => setCycle(e.target.value), handleCycleChange)}
              displayEmpty
            >
              {getQuarters().map((quarter, index) => (
                <MenuItem key={index} value={quarter.value}>
                  {quarter.label}
                </MenuItem>
              ))}
            </StyledSelect>
          </StyledFormControl>
        </Grid>

        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Scope:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <StyledFormControl>
            <StyledSelect
              value={scope}
              error={!!errors.scope}
              helperText={errors.scope}
              onChange={((e) => setScope(e.target.value), handleScopeChange)}
              displayEmpty
            >
              <MenuItem value={'Team'}>Team</MenuItem>
              <MenuItem value={'Individual'}>Individual</MenuItem>
            </StyledSelect>
          </StyledFormControl>
        </Grid>

        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Confidence Level:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <StyledFormControl>
            <StyledSelect
              value={confidentLevel}
              error={!!errors.confidentLevel}
              helperText={errors.confidentLevel}
              onChange={
                ((e) => setConfidentLevel(e.target.value),
                handleConfidenceChange)
              }
              displayEmpty
            >
              <MenuItem value={'High'}>High</MenuItem>
              <MenuItem value={'Medium'}>Medium</MenuItem>
              <MenuItem value={'Low'}>Low</MenuItem>
            </StyledSelect>
          </StyledFormControl>
        </Grid>
        <Grid item xs={3}>
          <StyledTitle required sx={{marginLeft: '30px'}} variant="subtitle1">
            Department:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <TextField
            disabled
            fullWidth
            variant="outlined"
            value={department}
            error={!!errors.department}
            helperText={errors.department}
            onChange={
              ((e) => setDepartment(e.target.value), handleDepartChange)
            }
            sx={{
              width: '150px',
              marginLeft: '7px',
              marginBottom: '5px',
              animation: errors.Content ? 'shake 0.3s' : 'none',
            }}
          />
        </Grid>
      </Grid>
      <Grid container spacing={2}>
        <Grid item xs={3}>
          <StyledTitle sx={{marginLeft: '30px'}} variant="subtitle1">
            Action Plan:
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <Box>
            <input
              accept=".doc,.docx,.pdf"
              style={{display: 'none'}}
              id="actionPlan-file"
              type="file"
              onChange={handleFileUpload}
            />
            <label htmlFor="actionPlan-file">
              <FileUploadButton
                sx={{
                  color: '#186BFF',
                  '& .MuiOutlinedInput-root': {
                    color: '#186BFF',
                  },
                }}
                component="span"
              >
                {actionPlan
                  ? actionPlan instanceof File
                    ? actionPlan.name
                    : actionPlan
                  : 'Upload File'}
              </FileUploadButton>
            </label>
            {errors.actionPlan && (
              <FormHelperText error>{errors.actionPlan}</FormHelperText>
            )}
          </Box>
        </Grid>
        <Grid item xs={3}>
          <StyledTitle sx={{marginLeft: '30px'}} variant="subtitle1">
            Expected Result:{' '}
          </StyledTitle>
        </Grid>
        <Grid item xs={9}>
          <TextField
            fullWidth
            name="expectedResult"
            variant="outlined"
            value={result}
            error={!!errors.expectedResult}
            helperText={errors.expectedResult}
            onChange={handleInputChange}
            sx={{
              marginTop: '15px',
              marginLeft: '7px',
              animation: errors.expectedResult ? 'shake 0.3s' : 'none',
            }}
          />
        </Grid>
      </Grid>

      <DialogActions>
        <StyledButton variant="outlined" color="info" onClick={onClose}>
          Cancel
        </StyledButton>
        <StyledButton
          variant="contained"
          color="primary"
          onClick={handleEditOkrRequest}
        >
          Edit
        </StyledButton>
      </DialogActions>
      <CustomDialog
        open={openParentAlignmentDialog}
        onCancel={handleCloseParentAlignmentDialog}
        actionName="Confirm"
        onConfirm={handleConfirmParent}
        maxWidth="md"
        viewDialog={true}
        showCloseButton={true}
        dialogContent={<ListParentAlignment onSelectOneRow={onSelectOneRow} />}
      />
    </div>
  )
}

export default EditOkrRequest
