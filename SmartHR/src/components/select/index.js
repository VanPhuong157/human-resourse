import {IconButton, InputAdornment, MenuItem, Select} from '@mui/material'
import {useState} from 'react'
import ClearIcon from '@mui/icons-material/Clear'

const SelectComponent = (props) => {
  const {dataSelect, labelId, name, label, value} = props
  const [selectValue, setSelectValue] = useState(value || '')
  return (
    <Select
      labelId={labelId}
      name={name}
      label={label}
      value={selectValue}
      onChange={(e) => setSelectValue(e.target.value)}
      sx={{
        '& .MuiSelect-icon': {
          display: selectValue ? 'none' : '',
        },
      }}
      endAdornment={
        selectValue && (
          <InputAdornment position="end">
            <IconButton onClick={() => setSelectValue('')} edge="end">
              <ClearIcon />
            </IconButton>
          </InputAdornment>
        )
      }
    >
      {dataSelect?.map((data) => (
        <MenuItem key={data.id} value={data.id}>
          {data.title ? data.title : data.value}
        </MenuItem>
      ))}
    </Select>
  )
}

export default SelectComponent
