import React, {useState, useEffect, useContext} from 'react'
import {useGetUserByDepart} from '../../../../pages/okr/requests/getOwnerOkr'
import {UserContext} from '../../../../context/UserContext'
import {useEditOwnerOKR} from '../../../../pages/okr/requests/editOwner'
import {showError, showSuccess, showToastSuccess} from '../../../../components/notification'
import {
  TextField,
  List,
  ListItemButton,
  ListItemText,
  Avatar,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
} from '@mui/material'
import { baseUrl } from '../../../../api/rootApi'

const ChangeOwner = ({data, open, onClose, refetchOkrDetail}) => {
  const [selectedUser, setSelectedUser] = useState(null)
  const [searchTerm, setSearchTerm] = useState('')

  const okrData = data?.data
  const {user} = useContext(UserContext)
  const {data: dataUserByDepart, refetch: refetchUserByDepart} =
    useGetUserByDepart(1, 100, user.departmentId)
  const {mutateAsync: mutateAsyncEditOwnerOkr} = useEditOwnerOKR({
    okrId: okrData?.id,
  })

  useEffect(() => {
    if (open) {
      refetchUserByDepart()
    }
    if (!open) {
      setSearchTerm('')
      setSelectedUser(null)
    }
  }, [open, refetchUserByDepart])

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value)
  }

  const handleUserSelect = (user) => {
    setSelectedUser(user)
  }

  const handleSubmit = () => {
    mutateAsyncEditOwnerOkr(selectedUser.userId)
      .then((response) => {
        showToastSuccess({message: response.data?.message})
        onClose()
        refetchOkrDetail()
      })
      .catch((err) => {
        showError(err.response?.data?.message)
      })
  }

  const filteredUsers =
    dataUserByDepart?.data?.items?.filter((user) =>
      user.fullName.toLowerCase().includes(searchTerm.toLowerCase()),
    ) || []

  return (
    <Dialog
      open={open}
      onClose={onClose}
      PaperProps={{
        style: {
          width: 500,
          height: 400,
        },
      }}
    >
      <DialogTitle sx={{color: '#186BFF'}}>Pick a person</DialogTitle>
      <DialogContent
        sx={{
          height: 'calc(100% - 64px)',
          overflowY: 'auto',
        }}
      >
        <TextField
          value={searchTerm}
          onChange={handleSearchChange}
          placeholder="Search for a person"
          fullWidth
          sx={{mb: 2}}
        />
        <List>
          {filteredUsers.length > 0 ? (
            filteredUsers.map((user) => (
              <ListItemButton
                key={user.id}
                onClick={() => handleUserSelect(user)}
                sx={{
                  bgcolor:
                    selectedUser && selectedUser.fullName === user.fullName
                      ? 'action.selected'
                      : 'transparent',
                  '&:hover': {
                    bgcolor: 'action.hover',
                  },
                }}
              >
                <Avatar
                  sx={{mr: 2}}
                  alt="User Avatar"
                  src={
                    user.avatar
                      ? baseUrl+ `${user.avatar}`
                      : 'https://img.freepik.com/premium-vector/default-avatar-profile-icon-social-media-user-image-gray-avatar-icon-blank-profile-silhouette-vector-illustration_561158-3467.jpg'
                  }
                />
                <ListItemText
                  primary={user.fullName}
                  secondary={`(${user.code})`}
                />
              </ListItemButton>
            ))
          ) : (
            <ListItemButton disabled>
              <ListItemText primary="No users found" />
            </ListItemButton>
          )}
        </List>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button onClick={handleSubmit} disabled={!selectedUser}>
          Submit
        </Button>
      </DialogActions>
    </Dialog>
  )
}

export default ChangeOwner
