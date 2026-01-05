import React, {useState, useEffect} from 'react'
import {
  DialogActions, Button, TextField, Select, MenuItem, FormControl,
  Typography, Box, Grid, FormHelperText, InputLabel, OutlinedInput, Chip
} from '@mui/material'
import {styled} from '@mui/material/styles'
import {rootApi} from '../../../../api/rootApi'
import path from '../../../../api/path'
import {showError, showSuccess} from '../../../../components/notification'
import CustomDialog from '../../../../components/customDialog'
import ListParentAlignment from './dialogParent/getParentAligment'
import {StatusCodes} from 'http-status-codes'
import useGetOkrRequest from '../../../../pages/okr/requests/getOkrRequest'

const StyledButton = styled(Button)(({theme}) => ({ margin: theme.spacing(1) }))
const StyledFormControl = styled(FormControl)(({theme}) => ({ margin: theme.spacing(1), minWidth: 120 }))
const Field = styled(TextField)({ marginBottom: '25px', width: '100%' })
const StyledTitle = styled(Typography)(({required}) => ({
  marginTop: '16px', justifyContent: 'center', display: 'flex', alignItems: 'center',
  '&::after': { content: required ? '"*"' : '""', color: 'red', marginLeft: 4, fontSize: '1rem' }
}))
const FileUploadButton = styled(Button)(({theme}) => ({
  borderRadius: 54, border: `1px solid ${theme.palette.text.primary}`, color: '#12448a',
  fontWeight: 200, width: '50%', justifyContent: 'flex-start', padding: '7px 12px',
  marginTop: 13, marginLeft: 7
}))
const shakeAnimation = `
@keyframes shake{0%{transform:translateX(0)}20%{transform:translateX(-10px)}40%{transform:translateX(10px)}60%{transform:translateX(-4px)}80%{transform:translateX(4px)}100%{transform:translateX(0)}}
`

const CreateOKR = ({onClose}) => {
  // ======= state theo BE =======
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')        // Content
  const [type, setType] = useState('')                      // Objective | KeyResult
  const [scope, setScope] = useState('')                    // Company | Team | Individual
  const [targetProgress, setTargetProgress] = useState(100) // default 100
  const [unitTarget, setUnitTarget] = useState('')          // UnitOfTarget
  const [targetNumber, setTargetNumber] = useState('')      // number string
  const [achieved, setAchieved] = useState('')              // number string
  const [approveStatus] = useState('Approve')               // default theo BE
  const [confidenceLevel, setConfidenceLevel] = useState('')// High | Medium | Low (string)
  const [result, setResult] = useState('')
  const [dueDate, setDueDate] = useState('')                // ISO date string (yyyy-mm-dd)
  const [company, setCompany] = useState('')
  const [note, setNote] = useState('')

  // liên kết
  const [departmentIds, setDepartmentIds] = useState([])    // list Guid
  const [ownerIds, setOwnerIds] = useState([])              // list Guid
  const [managerIds, setManagerIds] = useState([])          // list Guid
  const [parentAlignment, setParentAlignment] = useState(null) // {id, title}

  // nguồn dữ liệu chọn
  const [departments, setDepartments] = useState([])
  const [users, setUsers] = useState([])

  // file
  const [actionPlan, setActionPlan] = useState(null)        // pdf

  // UI & errors
  const [formErrors, setFormErrors] = useState({})
  const [errors, setErrors] = useState({
    title: '', description: '', targetNumber: '', achieved: '', unitTarget: '',
    type: '', scope: '', department: '', parentAlignment: '', confidenceLevel: '',
    actionPlan: '', expectedResult: ''
  })
  const [openParentAlignmentDialog, setOpenParentAlignmentDialog] = useState(false)

  // context
  const roleUser = localStorage.getItem('role')
  const departmentUser = localStorage.getItem('departmentId')
  const {refetch: refetchOkrRequest} = useGetOkrRequest(1, 100, '', departmentUser)

  // ======= load data =======
  useEffect(() => { (async () => {
      try {
        const res = await rootApi.get(path.department.getDepartments())
        setDepartments(res.data?.items || [])
      } catch (e) { showError(e) }
    })()
  }, [])
  useEffect(() => { (async () => {
      try {
        const res = await rootApi.get(path.user?.getUsers?.() ?? '/users')
        setUsers(res.data?.items || res.data || [])
      } catch (e) { console.warn('load users failed', e) }
    })()
  }, [])

  // ======= helpers =======
  const userName = (id) => users.find(u => u.id === id)?.fullName || id
  const handleOpenParentAlignmentDialog = () => setOpenParentAlignmentDialog(true)
  const handleCloseParentAlignmentDialog = () => setOpenParentAlignmentDialog(false)
  const onSelectOneRow = (row) => { if (row){ setParentAlignment(row); handleCloseParentAlignmentDialog() } }

  useEffect(() => { if (formErrors) setErrors(prev => ({...prev, ...formErrors})) }, [formErrors])
  useEffect(() => {
    if (parseFloat(achieved) > 0 && !parentAlignment) {
      setErrors(prev => ({...prev, parentAlignment: 'Parent Alignment is required when Achieved is greater than 0'}))
    } else if (parentAlignment) {
      setErrors(prev => ({...prev, parentAlignment: ''}))
    }
  }, [achieved, parentAlignment])

  const validateField = (name, value) => {
    let error = ''
    switch (name) {
      case 'title':
        if (!value) error = 'Title is required'
        else if (value.length > 200) error = 'Title cannot exceed 200 characters'
        break
      case 'description':
        if (!value) error = 'Main Result & Content is required'
        else if (value.length > 2000) error = 'Main Result & Content cannot exceed 2000 characters'
        break
      case 'targetNumber':
        if (!/^\d+$/.test(value) || parseFloat(value) <= 0) error = 'Target Number must be an integer greater than 0'
        else if (parseFloat(value) > 1000000000) error = 'Target Number cannot exceed 1,000,000,000'
        else if (achieved && parseFloat(value) < parseFloat(achieved)) error = 'Target Number must be ≥ Achieved'
        break
      case 'achieved':
        if (!/^\d+$/.test(value) || parseFloat(value) < 0) error = 'Achieved must be an integer ≥ 0'
        else if (parseFloat(value) > 1000000000) error = 'Achieved cannot exceed 1,000,000,000'
        else if (targetNumber && parseFloat(value) > parseFloat(targetNumber)) error = 'Achieved must be ≤ Target Number'
        break
      case 'unitTarget':
        if (!value) error = 'Unit of Target is required'
        else if (value.length > 10) error = 'Unit of Target cannot exceed 10 characters'
        break
      case 'expectedResult':
        if (value.length > 2000) error = 'Expected Result cannot exceed 2000 characters'
        break
      default: break
    }
    return error
  }

  const handleInputChange = (e) => {
    const {name, value} = e.target
    setErrors(prev => ({...prev, [name]: validateField(name, value)}))
    switch (name) {
      case 'title': if (value.length <= 200) setTitle(value); break
      case 'description': if (value.length <= 2000) setDescription(value); break
      case 'unitTarget': if (value.length <= 10) setUnitTarget(value); break
      case 'targetNumber': setTargetNumber(value); break
      case 'achieved':
        setAchieved(value)
        if (parseFloat(value) > 0 && !parentAlignment) {
          setErrors(prev => ({...prev, parentAlignment: 'Parent Alignment is required when Achieved is greater than 0'}))
        } else setErrors(prev => ({...prev, parentAlignment: ''}))
        break
      case 'expectedResult': if (value.length <= 2000) setResult(value); break
      case 'company': setCompany(value); break
      case 'note': setNote(value); break
      default: break
    }
  }

  const handleTypeChange = (e) => {
    const v = e.target.value
    setType(v)
    if (v === 'KeyResult' && !parentAlignment) {
      setErrors(prev => ({...prev, type: 'Must select Parent Alignment of OKR to select KeyResult'}))
    } else setErrors(prev => ({...prev, type: ''}))
  }
  const handleScopeChange = (e) => {
    const v = e.target.value
    setScope(v)
    setErrors(prev => ({...prev, scope: v ? '' : 'Scope is required'}))
  }
  const handleConfidenceChange = (e) => {
    const v = e.target.value
    setConfidenceLevel(v)
    setErrors(prev => ({...prev, confidenceLevel: v ? '' : 'Confidence Level is required'}))
  }
  const handleDueDateChange = (e) => setDueDate(e.target.value)

  const handleDepartmentsChange = (e) => {
    const v = e.target.value
    const arr = typeof v === 'string' ? v.split(',') : v
    setDepartmentIds(arr)
    setErrors(prev => ({...prev, department: (arr && arr.length) ? '' : 'At least 1 Department is required'}))
  }
  const handleOwnersChange = (e) => setOwnerIds(typeof e.target.value === 'string' ? e.target.value.split(',') : e.target.value)
  const handleManagersChange = (e) => setManagerIds(typeof e.target.value === 'string' ? e.target.value.split(',') : e.target.value)

  const handleFileUpload = (e) => {
    const file = e.target.files[0]
    const maxSize = 10 * 1024 * 1024
    const valid = 'application/pdf'
    if (file) {
      if (file.size > maxSize) { setErrors(prev => ({...prev, actionPlan: 'The file size must not exceed 10 MB.'})); setActionPlan(null) }
      else if (file.type !== valid) { setErrors(prev => ({...prev, actionPlan: 'Only PDF files are allowed.'})); setActionPlan(null) }
      else { setActionPlan(file); setErrors(prev => ({...prev, actionPlan: ''})) }
    }
  }

  const handleCreateOKR = async () => {
    const errs = {}

    // required tối thiểu khớp BE
    if (!title) errs.title = 'Title is required'
    if (!description) errs.description = 'Main Result & Content is required'
    if (!targetNumber) errs.targetNumber = 'Target Number is required'
    if (!achieved && achieved !== 0 && achieved !== '0') errs.achieved = 'Achieved is required'
    if (!unitTarget) errs.unitTarget = 'Unit of Target is required'
    if (!type) errs.type = 'Type is required'
    if (!scope) errs.scope = 'Scope is required'
    if (!confidenceLevel) errs.confidenceLevel = 'Confidence Level is required'
    if (!departmentIds || departmentIds.length === 0) errs.department = 'At least 1 Department is required'
    if (type === 'KeyResult' && !parentAlignment) errs.type = 'Must select Parent Alignment of OKR to select KeyResult'
    if (parseFloat(achieved) > 0 && !parentAlignment) errs.parentAlignment = 'Parent Alignment is required when Achieved is greater than 0'

    // số học
    if (targetNumber && achieved && parseFloat(achieved) > parseFloat(targetNumber)) {
      errs.achieved = 'Achieved must be ≤ Target Number'
    }

    if (Object.keys(errs).length) { setFormErrors(errs); return }

    const formData = new FormData()
    // ===== gửi đúng tên field theo BE OKRCreateDTO =====
    formData.append('ParentId', parentAlignment?.id || '')
    formData.append('Title', title)
    formData.append('Content', description)
    formData.append('Type', type)
    formData.append('Scope', scope)

    formData.append('TargetProgress', String(targetProgress ?? 100)) // giữ 100
    formData.append('UnitOfTarget', unitTarget)
    formData.append('TargetNumber', String(targetNumber))
    formData.append('Achieved', String(achieved))

    formData.append('ApproveStatus', 'Approve') // theo BE
    formData.append('ConfidenceLevel', confidenceLevel) // string

    formData.append('Result', result ?? '')
    if (dueDate) formData.append('DueDate', new Date(dueDate).toISOString())
    formData.append('Company', company ?? '')
    formData.append('Note', note ?? '')

    // lists
    ;(departmentIds || []).forEach(id => formData.append('DepartmentIds', id))
    ;(ownerIds || []).forEach(id => formData.append('OwnerIds', id))
    ;(managerIds || []).forEach(id => formData.append('ManagerIds', id))

    if (actionPlan) formData.append('ActionPlanFile', actionPlan)

    try {
      const res = await rootApi.post(path.okr.CreateOkr, formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
      })
      if (res.data?.code === StatusCodes.OK) {
        showSuccess({message: res.data?.message})
        setFormErrors({})
        onClose?.()
        refetchOkrRequest()
      } else if (res.data?.code === StatusCodes.BAD_REQUEST) {
        showError({message: res.data?.message})
      } else {
        showError({message: 'Create failed'})
      }
    } catch (err) {
      showError(err?.response?.data?.message || 'Create failed')
    }
  }

  return (
    <div>
      <Field
        required placeholder="Untitled" variant="filled" name="title" value={title}
        error={!!errors.title} helperText={errors.title} onChange={handleInputChange}
        sx={{
          '& .MuiInputBase-input::placeholder': { fontSize: '35px' },
          '& .MuiInputBase-input': { fontSize: '35px', color: '#1277B0', ...(errors.title && { borderColor: 'red', animation: 'shake 0.5s' }) }
        }}
      />
      <style>{shakeAnimation}</style>

      <Grid container spacing={2}>
        {/* Content */}
        <Grid item xs={3}><StyledTitle required sx={{ml:'30px'}} variant="subtitle1">Mô tả công việc:</StyledTitle></Grid>
        <Grid item xs={9}>
          <TextField fullWidth name="description" variant="outlined" value={description}
            error={!!errors.description} helperText={errors.description} onChange={handleInputChange}
            sx={{ mt:'15px', ml:'7px', animation: errors.description ? 'shake 0.3s':'none' }}/>
        </Grid>

        {/* TargetNumber */}
        <Grid item xs={3}><StyledTitle required sx={{ml:'30px'}} variant="subtitle1">Mục tiêu:</StyledTitle></Grid>
        <Grid item xs={9}>
          <TextField fullWidth name="targetNumber" variant="outlined" value={targetNumber}
            error={!!errors.targetNumber} helperText={errors.targetNumber} onChange={handleInputChange}
            sx={{ width:190, mt:'15px', ml:'7px', animation: errors.targetNumber ? 'shake 0.3s':'none' }}/>
        </Grid>

        {/* Achieved */}
        <Grid item xs={3}><StyledTitle required sx={{ml:'30px'}} variant="subtitle1">Đã đạt được:</StyledTitle></Grid>
        <Grid item xs={9}>
          <TextField fullWidth name="achieved" variant="outlined" value={achieved}
            error={!!errors.achieved} helperText={errors.achieved} onChange={handleInputChange}
            sx={{ width:190, mt:'15px', ml:'7px', animation: errors.achieved ? 'shake 0.3s':'none' }}/>
        </Grid>

        {/* UnitOfTarget */}
        <Grid item xs={3}><StyledTitle required sx={{ml:'30px'}} variant="subtitle1">Đơn vị:</StyledTitle></Grid>
        <Grid item xs={9}>
          <TextField fullWidth name="unitTarget" variant="outlined" value={unitTarget}
            error={!!errors.unitTarget} helperText={errors.unitTarget} onChange={handleInputChange}
            sx={{ width:190, mt:'15px', ml:'7px', animation: errors.unitTarget ? 'shake 0.3s':'none' }}/>
        </Grid>

        {/* Parent Alignment -> ParentId */}
        <Grid item xs={3}><StyledTitle sx={{ml:'30px'}} variant="subtitle1">Công việc cha:</StyledTitle></Grid>
        <Grid item xs={9}>
          <TextField fullWidth variant="outlined" placeholder="Click to select a Parent Alignment"
            value={parentAlignment?.title || ''} error={!!errors.parentAlignment} helperText={errors.parentAlignment}
            onClick={() => setOpenParentAlignmentDialog(true)} InputProps={{readOnly:true}}
            sx={{ width:400, mt:'15px', ml:'7px', animation: errors.parentAlignment ? 'shake 0.3s':'none',
              '& .MuiOutlinedInput-root': { color:'#186BFF', ...(errors.parentAlignment && { borderColor: 'red' }) } }}/>
        </Grid>

        {/* Type */}
        <Grid item xs={3}><StyledTitle required sx={{ml:'30px'}} variant="subtitle1">Loại:</StyledTitle></Grid>
        <Grid item xs={9}>
          <StyledFormControl fullWidth error={!!errors.type}>
            <Select value={type} onChange={handleTypeChange} displayEmpty sx={{height:55, width:180}}>
              <MenuItem value={'Công việc chung'}>Công việc chung</MenuItem>
              <MenuItem value={'Công việc cá nhân'}>Công việc cá nhân</MenuItem>
            </Select>
            <FormHelperText>{errors.type}</FormHelperText>
          </StyledFormControl>
        </Grid>

        {/* Scope */}
        <Grid item xs={3}><StyledTitle required sx={{ml:'30px'}} variant="subtitle1">Phạm vi:</StyledTitle></Grid>
        <Grid item xs={9}>
          <StyledFormControl fullWidth error={!!errors.scope}>
            <Select value={scope} onChange={handleScopeChange} displayEmpty sx={{height:55, width:180}}>
              <MenuItem value={'Company'}>Công ty</MenuItem>
              <MenuItem value={'Team'}>Phòng ban</MenuItem>
              <MenuItem value={'Individual'}>Cá nhân</MenuItem>
            </Select>
            <FormHelperText>{errors.scope}</FormHelperText>
          </StyledFormControl>
        </Grid>

        {/* Departments (multi) */}
        <Grid item xs={3}><StyledTitle required sx={{ml:'30px'}} variant="subtitle1">Phòng ban:</StyledTitle></Grid>
        <Grid item xs={9}>
          <FormControl sx={{m:1, width:400}} error={!!errors.department}>
            <InputLabel id="dept-multi-label">Chọn phòng ban</InputLabel>
            <Select
              labelId="dept-multi-label" multiple value={departmentIds} onChange={handleDepartmentsChange}
              input={<OutlinedInput label="Select departments" />}
              renderValue={(selected) => (
                <Box sx={{display:'flex', flexWrap:'wrap', gap:0.5}}>
                  {selected.map((value) => {
                    const d = departments.find(x => x.id === value)
                    return <Chip key={value} label={d?.name || value}/>
                  })}
                </Box>
              )}
            >
              {departments
                .filter(d => (roleUser === 'Admin' || roleUser === 'BOD') ? true : d.id === departmentUser)
                .map(d => (<MenuItem key={d.id} value={d.id}>{d.name}</MenuItem>))}
            </Select>
            <FormHelperText>{errors.department}</FormHelperText>
          </FormControl>
        </Grid>

        {/* Owners (multi) */}
        <Grid item xs={3}><StyledTitle sx={{ml:'30px'}} variant="subtitle1">Người thực hiện:</StyledTitle></Grid>
        <Grid item xs={9}>
          <FormControl sx={{m:1, width:400}}>
            <InputLabel id="owner-multi-label">Chọn người thực hiện</InputLabel>
            <Select
              labelId="owner-multi-label" multiple value={ownerIds} onChange={handleOwnersChange}
              input={<OutlinedInput label="Select owners"/>}
              renderValue={(selected) => (
                <Box sx={{display:'flex', flexWrap:'wrap', gap:0.5}}>
                  {selected.map(id => <Chip key={id} label={userName(id)} />)}
                </Box>
              )}
            >
              {users.map(u => (<MenuItem key={u.id} value={u.id}>{u.fullName || u.email || u.id}</MenuItem>))}
            </Select>
          </FormControl>
        </Grid>

        {/* Managers (multi) */}
        <Grid item xs={3}><StyledTitle sx={{ml:'30px'}} variant="subtitle1">Người quản lý:</StyledTitle></Grid>
        <Grid item xs={9}>
          <FormControl sx={{m:1, width:400}}>
            <InputLabel id="manager-multi-label">Chọn người quản lý</InputLabel>
            <Select
              labelId="manager-multi-label" multiple value={managerIds} onChange={handleManagersChange}
              input={<OutlinedInput label="Select managers"/>}
              renderValue={(selected) => (
                <Box sx={{display:'flex', flexWrap:'wrap', gap:0.5}}>
                  {selected.map(id => <Chip key={id} label={userName(id)} />)}
                </Box>
              )}
            >
              {users.map(u => (<MenuItem key={u.id} value={u.id}>{u.fullName || u.email || u.id}</MenuItem>))}
            </Select>
          </FormControl>
        </Grid>

        {/* ConfidenceLevel (string) */}
        <Grid item xs={3}><StyledTitle required sx={{ml:'30px'}} variant="subtitle1">Priority:</StyledTitle></Grid>
        <Grid item xs={9}>
          <StyledFormControl fullWidth error={!!errors.confidenceLevel}>
            <Select value={confidenceLevel} onChange={handleConfidenceChange} displayEmpty sx={{height:55, width:180}}>
              <MenuItem value={'High'}>High</MenuItem>
              <MenuItem value={'Medium'}>Medium</MenuItem>
              <MenuItem value={'Low'}>Low</MenuItem>
            </Select>
            <FormHelperText>{errors.confidenceLevel}</FormHelperText>
          </StyledFormControl>
        </Grid>

        {/* TargetProgress (ẩn – gửi 100) */}
        <input type="hidden" name="targetProgress" value={targetProgress} />

        {/* DueDate */}
        <Grid item xs={3}><StyledTitle sx={{ml:'30px'}} variant="subtitle1">Ngày hết hạn:</StyledTitle></Grid>
        <Grid item xs={9}>
          <TextField
            type="date" value={dueDate} onChange={handleDueDateChange}
            sx={{ width:220, mt:'15px', ml:'7px' }}
            InputLabelProps={{shrink:true}}
          />
        </Grid>

        {/* Company */}
        <Grid item xs={3}><StyledTitle sx={{ml:'30px'}} variant="subtitle1">Công ty:</StyledTitle></Grid>
        <Grid item xs={9}>
          <TextField fullWidth name="company" value={company} onChange={handleInputChange}
            sx={{ width:300, mt:'15px', ml:'7px' }}/>
        </Grid>

        {/* Note */}
        <Grid item xs={3}><StyledTitle sx={{ml:'30px'}} variant="subtitle1">Note:</StyledTitle></Grid>
        <Grid item xs={9}>
          <TextField fullWidth name="note" value={note} onChange={handleInputChange}
            sx={{ width:400, mt:'15px', ml:'7px' }}/>
        </Grid>
      </Grid>

      <DialogActions>
        <StyledButton variant="outlined" color="info" onClick={onClose}>Hủy</StyledButton>
        <StyledButton variant="contained" color="primary" onClick={handleCreateOKR}>Tạo</StyledButton>
      </DialogActions>

      <CustomDialog
        open={openParentAlignmentDialog}
        onCancel={handleCloseParentAlignmentDialog}
        actionName="Confirm"
        onConfirm={handleCloseParentAlignmentDialog}
        maxWidth="md"
        viewDialog={true}
        showCloseButton={true}
        dialogContent={<ListParentAlignment onSelectOneRow={onSelectOneRow} />}
      />
    </div>
  )
}

export default CreateOKR
