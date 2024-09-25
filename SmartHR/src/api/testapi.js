import * as utils from './utils'
import { rootApi } from './rootApi'
import path from './path'

export const login = (data) =>
  new Promise((resolve, reject) => {
    rootApi
      .post(path.auth.login, data)
      .then((res) => resolve(utils.filterUserInfo(res.data)))
      .catch((err) => reject(err))
  })
