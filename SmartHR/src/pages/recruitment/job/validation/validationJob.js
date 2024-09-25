const validateTitle = (title) => {
    return title ? '' : 'This field is required';
  };
  
  const validateNumber = (value, fieldName) => {
    return value && isNaN(Number(value)) ? `Invalid ${fieldName}. Must be a number.` : '';
  };
  
  const validateForm = (data) => {
    let errors = {};
  
    errors.title = validateTitle(data.title);
    errors.salary = validateNumber(data.salary, 'Salary');
    errors.experienceYear = validateNumber(data.experienceYear, 'Experience Year');
    errors.numberOfRecruits = validateNumber(data.numberOfRecruits, 'Number Of Recruits');
    errors.department = data.department ? '' : 'Department is required';
    errors.type = data.type ? '' : 'Type is required';
    errors.expiryDate = data.expiryDate ? '' : 'Expiry Date is required';
  
    return errors;
  };
  
  export { validateForm };
  