import Cookies from 'js-cookie';


export function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1
  }
  if (b[orderBy] > a[orderBy]) {
    return 1
  }
  return 0
}

export function getComparator(order, orderBy) {
  return order === 'desc'
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy)
}

export function stableSort(array, comparator) {
  const stabilizedThis = array.map((el, index) => [el, index])
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0])
    if (order !== 0) {
      return order
    }
    return a[1] - b[1]
  })
  return stabilizedThis.map((el) => el[0])
}



// Create an authentication cookie
export const createAuthCookie = (token) => {
  Cookies.set('userAuth', token, { path: '/', sameSite: 'Lax' });
};

// Delete the authentication cookie
export const deleteAuthCookie = () => {
  Cookies.remove('userAuth');
};



// utils.js or index.js inside the utils folder
export const convertDateFormat = (dateStr) => {
  const [day, month, year] = dateStr.split('/');
  return `${year}-${month}-${day}`;
};
