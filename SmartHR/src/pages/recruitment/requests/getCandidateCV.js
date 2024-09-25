import { useQuery } from 'react-query';
import path from '../../../api/path';
import { rootApi } from '../../../api/rootApi';

export default function useGetCandidateCV({ candidateId }) {
  const queryFn = () => rootApi.get(path.candidate.getCandidateCV(candidateId));
  const enabled = !!candidateId;
  const data = useQuery(['get-candidate-cv', candidateId], queryFn, { enabled });
  return data;
}
