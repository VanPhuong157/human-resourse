import { rootApi } from "../../../api/rootApi"
import path from "../../../api/path"
import { useMutation } from "react-query"


const useLogin = () => {
    return useMutation('login', (data) => {
      return rootApi.post(path.auth.login, data)
    })
  }
  
  export default useLogin

  
