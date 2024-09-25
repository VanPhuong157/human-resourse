
import {useQuery,useMutation} from 'react-query'
import path from '../../../../api/path'
import { rootApi } from '../../../../api/rootApi'


export const useGetPosts = (pageIndex , pageSize,filters) => {
    const data = useQuery(
        ['get-posts', pageIndex, pageSize,filters],
      () => rootApi.get(path.post.getPosts(pageIndex, pageSize,filters)),
      {
        keepPreviousData:true,
      }
    )
    
    return data
  }

  export const useAddPost = () =>{
    return useMutation('add-post', (formData) => {
      return rootApi.post(path.post.addPost,formData)
    })
  }

  export const useEditPost = ({postId}) => {
    return useMutation('edit-post', (formData) => {
      return rootApi.put(path.post.editPost({postId}),formData)
    })
  }
  export const useGetDepartments = () => {
    const data = useQuery(
        ['get-departments'],
      () => rootApi.get(path.department.getDepartments()),
      {
        keepPreviousData:true,
      }
    )
    
    return data
  }
