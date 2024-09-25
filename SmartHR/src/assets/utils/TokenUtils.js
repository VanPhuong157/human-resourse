export function  decodeJWT(token) {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      return JSON.parse(window.atob(base64));
    } catch (error) {
      console.error('Error decoding JWT:', error);
      return null;
    }
  }

  export function saveTokenToLocalStorage(token) {
    localStorage.setItem('accessToken', token);
  }
  
//   export function getTokenFromLocalStorage() {
//     return localStorage.getItem('accessToken');
//   }
  
//   export function removeTokenFromLocalStorage() {
//     localStorage.removeItem('accessToken');
//   }

export function isTokenExpired(expDate) {
    const currentTime = new Date().getTime();
    return expDate < currentTime;
  }