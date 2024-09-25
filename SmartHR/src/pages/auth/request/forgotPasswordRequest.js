import { rootApi } from "../../../api/rootApi"
import path from "../../../api/path"
import { useMutation } from "react-query"


const useForgotPassword = ({email}) => {
  return useMutation('forgot-password', (data) => {
    return rootApi.put(path.auth.forgotPassword({email}), data)
  })
}

export default useForgotPassword
