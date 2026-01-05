// editors/SingleSelectEditor.jsx
import {forwardRef, useImperativeHandle, useRef, useState} from 'react'
import {Box, TextField, Autocomplete} from '@mui/material'

const SingleSelectEditor = rf((props, ref) => {
  const { value, colDef, node, api } = props
  const p = colDef?.cellEditorParams || {}
  const [open, setOpen] = useState(true)
  const [loading, setLoading] = useState(false)
  const [options, setOptions] = useState(p.options || [])
  const [inputValue, setInputValue] = useState('')
  const [selected, setSelected] = useState(() => p.toOption?.(value) ?? value ?? null)

  useImperativeHandle(ref, () => ({
    getValue: () => {
      if (p.onBeforeCommit) p.onBeforeCommit(selected, props)
      return selected
    },
    isPopup: () => true,
  }))

  useEffect(() => {
    let alive = true
    if (!p.fetch) return
    setLoading(true)
    p.fetch('', node?.data).then(list => alive && setOptions(list || []))
      .finally(() => alive && setLoading(false))
    return () => { alive = false }
  }, [])

  const commitClose = () => { setOpen(false); api?.stopEditing(false) }
  const cancelClose = () => { setOpen(false); api?.stopEditing(true) }

  return (
    <Box sx={{ p: 1, width: 320, bgcolor: '#fff' }}>
      <Autocomplete
        autoFocus
        open={open}
        onOpen={() => setOpen(true)}
        onClose={commitClose}                 // chỉ khi đóng mới commit
        disableCloseOnSelect                  // chọn không đóng popup
        options={options}
        getOptionLabel={(o) => (typeof o === 'string' ? o : o?.label ?? '')}
        isOptionEqualToValue={(a,b)=> (a?.value ?? a) === (b?.value ?? b)}
        value={selected}
        onChange={(_, v) => setSelected(v)}   // chỉ set state, KHÔNG commit
        inputValue={inputValue}
        onInputChange={(_, v) => {
          setInputValue(v)
          if (p.fetch) {
            setLoading(true)
            p.fetch(v, node?.data).then(list => setOptions(list || []))
              .finally(() => setLoading(false))
          }
        }}
        onKeyDown={(e) => { if (e.key === 'Escape') cancelClose() }} // Enter không commit
        renderInput={(params) => (
          <TextField
            {...params}
            size="small"
            placeholder={p.placeholder || 'Chọn...'}
            InputProps={{
              ...params.InputProps,
              endAdornment: (
                <>
                  {loading ? <CircularProgress size={18}/> : null}
                  {params.InputProps.endAdornment}
                </>
              ),
            }}
          />
        )}
      />
    </Box>
  )
})

export default SingleSelectEditor
