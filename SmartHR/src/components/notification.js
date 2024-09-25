import {Store as store} from 'react-notifications-component'
import Swal from 'sweetalert2'
export const parseSuccess = (success) => {
  if (!success) return ''
  if (typeof success === 'string') {
    return success
  }
  const {data} = success.response || {}
  if (data && data.success && data.success.default) {
    const successes = data.success.default
    let message = ''
    Object.keys(successes).forEach((key) => {
      message += successes[key]
    })
    return message
  }

  if (data && data.success && data.success.cause) {
    return data.success.cause
  }

  return success.message
}
export const showSuccess = (message) => {
  const successMessage = parseSuccess(message)
  Swal.fire('Success!', successMessage, 'success')
}

export const showToastSuccess = (message) => {
  // const successMessage = JSON.stringify(parseSuccess(message))
  const successMessage = parseSuccess(message)
  return store.addNotification({
    message: successMessage,
    type: 'success',
    insert: 'top',
    container: 'top-right',
    animationIn: ['animate__animated', 'animate__fadeIn'],
    animationOut: ['animate__animated', 'animate__fadeOut'],
    dismiss: {
      duration: 3000,
    },
  })
}

export const parseError = (error) => {
  if (!error) return ''
  if (typeof error === 'string') {
    return error
  }
  const {data} = error.response || {}
  if (data && data.error && data.error.default) {
    const errors = data.error.default
    let message = ''
    Object.keys(errors).forEach((key) => {
      message += errors[key]
    })
    return message
  }

  if (data && data.error && data.error.cause) {
    return data.error.cause
  }

  return error.message
}

export const showError = (error) => {
  // const errMessage = JSON.stringify(parseError(error))
  const errMessage = parseError(error)

  store.addNotification({
    title: 'Error',
    message: errMessage || 'General Error',
    type: 'danger',
    insert: 'top',
    container: 'top-right',
    animationIn: ['animate__animated', 'animate__fadeIn'],
    animationOut: ['animate__animated', 'animate__fadeOut'],
    dismiss: {
      duration: 5000,
    },
  })
}

export const showWarning = ({message}) => {
  store.addNotification({
    title: 'Warning',
    message: message || 'General Warning',
    type: 'warning',
    insert: 'top',
    container: 'top-right',
    animationIn: ['animate__animated', 'animate__fadeIn'],
    animationOut: ['animate__animated', 'animate__fadeOut'],
    dismiss: {
      duration: 5000,
    },
  })
}
