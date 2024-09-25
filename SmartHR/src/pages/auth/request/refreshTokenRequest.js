import { rootApi } from "../../../api/rootApi"
import path from "../../../api/path"
import { useMutation } from "react-query"


const useRefreshToken = ({token}) => {
  return useMutation('refresh-token', (data) => {
    return rootApi.post(path.auth.refreshToken({token}), data)
  })
}

export default useRefreshToken
