import React, {useState, useCallback, useEffect} from 'react'
import {
  AppBar,
  Toolbar,
  Typography,
  Button,
  Box,
  Container,
  Card,
  CardContent,
  Paper,
  Grid,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  IconButton,
  Link,
} from '@mui/material'
import {styled} from '@mui/system'
import CloseIcon from '@mui/icons-material/Close'
// import bannerImage from '../../assets/images/homepage/banner2.png'
import logo from '../../assets/logo.png'
import NavigateBefore from '@mui/icons-material/NavigateBefore'
import NavigateNext from '@mui/icons-material/NavigateNext'
import {showError, showSuccess} from '../../components/notification'
import {useGetPosts} from './request/JobListRequest'
import {baseUrl, rootApi} from '../../api/rootApi'
import path from '../../api/path'
import InstagramIcon from '@mui/icons-material/Instagram'
import YouTubeIcon from '@mui/icons-material/YouTube'
import TwitterIcon from '@mui/icons-material/Twitter'
import FacebookIcon from '@mui/icons-material/Facebook'
import LinkedInIcon from '@mui/icons-material/LinkedIn'
import {useGetReasons} from '../admin/homepagesetting/request/GetReasonsRequest'
import {useGetHomePageActive} from '../../pages/admin/homepagesetting/request/getHomePageActive'
import {useNavigate} from 'react-router-dom'
import AccessTimeIcon from '@mui/icons-material/AccessTime'
import LocationOnIcon from '@mui/icons-material/LocationOn'
import PaidIcon from '@mui/icons-material/Paid'
import {StatusCodes} from 'http-status-codes'
import Cookies from 'js-cookie'

const FullScreenBackground = styled(Box)(({backgroundImage}) => ({
  backgroundImage: backgroundImage
    ? `url(${backgroundImage})`
    : 'linear-gradient(to right, #28a745, #007bff)',
  backgroundSize: 'cover',
  backgroundPosition: 'center',
  minHeight: '100vh',
  display: 'flex',
  flexDirection: 'column',
}))

const TransparentAppBar = styled(AppBar)(({theme, transparent}) => ({
  backgroundColor: transparent
    ? 'rgba(255, 255, 255, 0.8)'
    : 'rgba(255, 255, 255, 0.1)',
  boxShadow: 'none',
  transition: 'background-color 0.3s ease',
  backdropFilter: transparent ? 'none' : 'blur(5px)',
}))

const Logo = styled('img')({
  height: '100px',
  width: '100px',
  marginRight: '20px',
})

const StyledButton = styled(Button)({
  color: 'white',
  marginLeft: '10px',
})
const StyledButtonHeader = styled(Button)({
  color: 'black',
  marginLeft: '10px',
})

const ContentContainer = styled(Container)({
  flexGrow: 1,
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
})

const TextContent = styled(Box)({
  color: 'white',
  maxWidth: '100%',
})

const StyledCard = styled(Card)({
  height: '100%',
  backgroundColor: '#F3E6DD',
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'space-between',
  borderRadius: '20px',
  width: '100%',
  paddingLeft: '10px',
  paddingRight: '10px',
})

const StyledButton1 = styled(Button)({
  borderRadius: '20px',
  textTransform: 'none',
})
const CustomNavigateBeforeIcon = styled(NavigateBefore)(({theme}) => ({
  width: '45px',
  height: '50px',
  '&:hover': {
    color: '#fff',
  },
}))
const CustomNavigateNextIcon = styled(NavigateNext)(({theme}) => ({
  width: '45px',
  height: '50px',
  '&:hover': {
    color: '#fff',
  },
}))
const StyledContainer = styled(Container)({
  display: 'flex',
  alignItems: 'center',
})
const StyledDialog = styled(Dialog)(({theme}) => ({
  '& .MuiDialog-paper': {
    borderRadius: '16px',
    backgroundColor: 'white',
    border: '1px solid #ccc',
  },
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
  '& .MuiDialogTitle-root': {
    fontSize: '24px',
    fontWeight: 'bold',
  },
}))

const StyledTextField = styled(TextField)(({theme, error}) => ({
  '& .MuiInputLabel-asterisk': {
    color: 'red',
  },
  '& .MuiOutlinedInput-root': {
    '& fieldset': {
      borderColor: error ? 'red' : 'default',
    },
    backgroundColor: 'white',
    border: '1px solid #ccc',
  },
  '& .MuiInputLabel-root': {
    fontSize: '16px',
  },
}))
const FileUploadButton = styled(Button)(({theme}) => ({
  border: '1px solid #ccc', // Add a border
  color: '#333333',
  fontSize: '16px',
  fontWeight: 200,
  width: '100%',
  justifyContent: 'flex-start',
  padding: '7px 12px',
  marginTop: '13px',
  backgroundColor: 'white', // Set background to white
  textTransform: 'none', // Loại bỏ việc viết hoa toàn bộ chữ

  '&:hover': {
    backgroundColor: '#f5f5f5', // Lighten background on hover
  },
}))
const SubmitButton = styled(Button)(({theme}) => ({
  borderRadius: '20px',
  backgroundColor: '#ff6b00',
  color: '#fff',
  padding: '13px 32px',
  fontSize: '20px',
  fontWeight: 'bold',
  '&:hover': {
    backgroundColor: '#e65c00',
  },
}))
const InputRow = styled(Box)({
  display: 'flex',
  justifyContent: 'space-between',
  gap: '20px',
})

const Header = ({dataHomePage, homePageAtiveId}) => {
  const [transparent, setTransparent] = useState(true)
  const [isCheckAuth, setIsCheckAuth] = useState(false)
  useEffect(() => {
    // Kiểm tra authentication khi component mount
    const userAuth = Cookies.get('userAuth')
    if (userAuth) {
      setIsCheckAuth(true)
    }
  }, []) // Chạy effect chỉ một lần khi component mount
  const srcImg = baseUrl + `/api/HomePages/download-image/${homePageAtiveId}`
  useEffect(() => {
    const handleScroll = () => {
      const isTop = window.scrollY < 100
      setTransparent(isTop)
    }

    window.addEventListener('scroll', handleScroll)

    return () => {
      window.removeEventListener('scroll', handleScroll)
    }
  }, [])

  const scrollToSection = (sectionId) => {
    const element = document.getElementById(sectionId)
    if (element) {
      element.scrollIntoView({behavior: 'smooth'})
    }
  }
  const navigate = useNavigate()
  const handleLoginClick = () => {
    navigate('/login')
  }
  const handleBackToSHRSClick = () => {
    navigate('/dashboard')
  }
  return (
    <FullScreenBackground backgroundImage={srcImg}>
      <TransparentAppBar
        position="fixed"
        color="transparent"
        elevation={0}
        transparent={transparent}
      >
        <Toolbar>
          <Logo src={logo} alt="FPT Software" />
          <Box sx={{flexGrow: 1}}>
            <StyledButtonHeader onClick={() => scrollToSection('recruitment')}>
              Recruitment Positions
            </StyledButtonHeader>
            <StyledButtonHeader onClick={() => scrollToSection('aboutUs')}>
              About Us
            </StyledButtonHeader>
            <StyledButtonHeader onClick={() => scrollToSection('footer')}>
              Contact
            </StyledButtonHeader>
          </Box>
          {isCheckAuth ? (
            <StyledButton
              variant="contained"
              color="warning"
              onClick={handleBackToSHRSClick}
            >
              Back To SHRS
            </StyledButton>
          ) : (
            <StyledButton
              variant="contained"
              color="warning"
              onClick={handleLoginClick}
            >
              Login
            </StyledButton>
          )}
        </Toolbar>
      </TransparentAppBar>
      <ContentContainer>
        <TextContent>
          <Typography
            variant="h2"
            component="h1"
            gutterBottom
            style={{color: '#EE7B28', fontWeight: 'bold'}}
          >
            {dataHomePage.title}
          </Typography>
          <Typography
            variant="h4"
            component="h2"
            gutterBottom
            style={{color: '#EE7B28', fontWeight: 'bold'}}
          >
            SHRS
          </Typography>
          <Typography
            variant="h6"
            style={{color: '#EE7B28', fontWeight: 'bold'}}
          >
            {dataHomePage.titleBody}
          </Typography>
        </TextContent>
        {/* <ImageContent>
          <img src={bannerImage} alt="Professional" style={{ maxWidth: '100%' }} />
        </ImageContent> */}
      </ContentContainer>
    </FullScreenBackground>
  )
}

const Recruitment = () => {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [detailsDialogOpen, setDetailsDialogOpen] = useState(false)
  const [selectedJob, setSelectedJob] = useState(null)
  // const navigate = useNavigate()
  const [errors, setErrors] = useState({
    FullName: '',
    PhoneNumber: '',
    Email: '',
    CvFile: '',
  })
  const [job] = useState([])
  const [currentSlide, setCurrentSlide] = useState(0)
  const [jobsInSlide, setJobsInSlide] = useState([])
  const PageSize = 6
  const JobTitle = styled(Typography)(({theme}) => ({
    color: theme.palette.text,
    fontWeight: 700,
    fontSize: '18px',
    lineHeight: '111%',
    letterSpacing: '0.15px',
  }))

  const JobCard = ({job, onApply, onDetails}) => (
    <StyledCard>
      <CardContent sx={{flexGrow: 1}}>
        <JobTitle variant="h6" component="div" gutterBottom>
          {job.title}
        </JobTitle>
        <Box sx={{display: 'flex', alignItems: 'center', mt: 1}}>
          <AccessTimeIcon sx={{mr: 1}} />
          <Typography variant="body2" color="text.secondary">
            {job.experienceYear === 0
              ? 'Không yêu cầu kinh nghiệm'
              : `${job.experienceYear} years of experience`}{' '}
          </Typography>
        </Box>
        <Box sx={{display: 'flex', alignItems: 'center', mt: 1}}>
          <PaidIcon sx={{mr: 1}} />
          <Typography variant="body2" color="text.secondary">
            {job.salary === 0
              ? 'Lương theo thỏa thuận'
              : `${job.salary} $/year`}{' '}
          </Typography>
        </Box>
        <Box sx={{display: 'flex', alignItems: 'center', mt: 1}}>
          <LocationOnIcon sx={{mr: 1}} />
          <Typography variant="body2" color="text.secondary">
            {job.departmentName}
          </Typography>
        </Box>
      </CardContent>
      <Box sx={{p: 2, display: 'flex', justifyContent: 'space-between'}}>
        <StyledButton1
          variant="outlined"
          color="primary"
          size="small"
          onClick={() => onDetails(job)}
        >
          Details
        </StyledButton1>
        <StyledButton1
          variant="contained"
          color="warning"
          size="small"
          onClick={() => onApply(job)}
        >
          Apply Now
        </StyledButton1>
      </Box>
    </StyledCard>
  )

  const handleOpenDialog = (job) => {
    setSelectedJob(job)
    setDialogOpen(true)
  }

  const handleCloseDialog = () => {
    setDialogOpen(false)
  }

  const handleOpenDetailsDialog = (job) => {
    setSelectedJob(job)
    setDetailsDialogOpen(true)
  }

  const handleCloseDetailsDialog = () => {
    setDetailsDialogOpen(false)
  }

  const [formData, setFormData] = useState({
    FullName: '',
    PhoneNumber: '',
    Email: '',
    CvFile: null,
  })

  const handleNextSlide = () => {
    setCurrentSlide(currentSlide + 1)
  }

  const handlePrevSlide = () => {
    setCurrentSlide(currentSlide - 1)
  }

  const {refetch} = useGetPosts(currentSlide + 1, PageSize)
  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      if (response && Array.isArray(response?.data?.data?.items)) {
        setJobsInSlide(response.data.data.items)
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetch])

  useEffect(() => {
    fetchData()
  }, [jobsInSlide, currentSlide, refetch, fetchData])

  const validateInput = (name, value) => {
    let error = ''

    if (name === 'FullName') {
      if (!value) error = 'Please enter your full name.'
      if (value.length > 50) {
        error = 'The Full Name must be between 1 and 50 characters long.'
      }
    }

    if (name === 'PhoneNumber') {
      const phoneNumberPattern =
        /^(0|\+84)(3[2-9]|5[689]|7[0|6-9]|8[1-5]|9[0-9])[0-9]{7}$/
      if (!value) {
        error = 'Please enter your phone number.'
      } else if (!phoneNumberPattern.test(value)) {
        error = 'Invalid phone number format.'
      }
    }

    if (name === 'Email') {
      const emailPattern = /^[^@\s]+@gmail\.com$/
      if (!value) {
        error = 'Please enter your email.'
      } else if (!emailPattern.test(value)) {
        error = 'Please enter a valid email address.'
      }
    }

    if (name === 'CvFile') {
      if (!value) {
        error = 'Please attach your CV file.'
      } else if (value.type !== 'application/pdf') {
        error = 'Only PDF files are allowed.'
      }
    }

    return error
  }

  const handleInputChange = (event) => {
    const {name, value} = event.target
    const error = validateInput(name, value)

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }))
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: error,
    }))
  }

  const handleFileUpload = (event) => {
    const file = event.target.files[0]
    const error = validateInput('CvFile', file)

    setFormData((prevData) => ({
      ...prevData,
      CvFile: file,
    }))
    setErrors((prevErrors) => ({
      ...prevErrors,
      CvFile: error,
    }))
  }

  const handleSubmit = async (event) => {
    event.preventDefault()

    const newErrors = {
      FullName: validateInput('FullName', formData.FullName),
      PhoneNumber: validateInput('PhoneNumber', formData.PhoneNumber),
      Email: validateInput('Email', formData.Email),
      CvFile: validateInput('CvFile', formData.CvFile),
    }

    setErrors(newErrors)

    if (Object.values(newErrors).some((error) => error !== '')) {
      showError({message: 'Please correct the errors before submitting.'})
      return
    }

    //Submit FormData
    const formDataToSend = new FormData()
    formDataToSend.append('FullName', formData.FullName)
    formDataToSend.append('PhoneNumber', formData.PhoneNumber)
    formDataToSend.append('Email', formData.Email)
    formDataToSend.append('CvFile', formData.CvFile)
    formDataToSend.append('JobPostId', selectedJob.id)
    try {
      const response = await rootApi.post(
        path.homepage.appylyCV(),
        formDataToSend,
        {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        },
      )
      showSuccess({message: 'Apply Successfully!'})
      handleCloseDialog()
    } catch (error) {
      console.log(error)
      if (error?.response?.status === StatusCodes.BAD_REQUEST) {
        const badRequestMessage = error.response?.data?.title
        showError({
          message: badRequestMessage,
        })
      } else {
        showError(error.response?.data?.message)
      }
    }
  }
  return (
    <>
      <Box id="recruitment" sx={{bgcolor: '#f5f5f5', py: 8, minHeight: '70vh'}}>
        <Container>
          <Typography
            variant="h4"
            component="h2"
            align="center"
            gutterBottom
            style={{color: '#EE7B28', fontWeight: 'bold'}}
          >
            Career opportunities
          </Typography>
          <Typography variant="h5" component="h3" align="center" gutterBottom>
            Just for you
          </Typography>
          <StyledContainer>
            <CustomNavigateBeforeIcon
              variant="contained"
              disabled={currentSlide === 0}
              onClick={handlePrevSlide}
              sx={{visibility: currentSlide === 0 ? 'hidden' : 'visible'}}
            ></CustomNavigateBeforeIcon>
            <Grid container spacing={4.5} sx={{mt: 3}}>
              {jobsInSlide.slice(0, 6).map(
                (
                  job,
                  index, // Chỉ lấy 6 job cho 2 hàng
                ) => (
                  <Grid item xs={12} sm={6} md={4} key={index}>
                    <JobCard
                      job={job}
                      onApply={() => handleOpenDialog(job)}
                      onDetails={() => handleOpenDetailsDialog(job)}
                    />
                  </Grid>
                ),
              )}
            </Grid>
            <CustomNavigateNextIcon
              variant="contained"
              disabled={currentSlide === Math.floor(job.length / PageSize) - 1}
              onClick={handleNextSlide}
              sx={{
                ml: 2,
                visibility:
                  jobsInSlide.length < PageSize ? 'hidden' : 'visible',
              }}
            ></CustomNavigateNextIcon>
          </StyledContainer>
        </Container>
      </Box>
      <StyledDialog
        open={dialogOpen}
        onClose={handleCloseDialog}
        aria-labelledby="application-form-dialog"
        maxWidth="md"
        fullWidth
      >
        <DialogTitle id="application-form-dialog">
          <Box display="flex" justifyContent="flex-end">
            <IconButton
              aria-label="close"
              onClick={handleCloseDialog}
              sx={{
                position: 'absolute',
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
              }}
            >
              <CloseIcon />
            </IconButton>
          </Box>
          <Box display="flex" flexDirection="column" alignItems="center">
            <Typography
              variant="h4"
              component="h1"
              sx={{
                color: '#f08538',
                fontWeight: 800,
                letterSpacing: '0.15px',
                lineHeight: '83%',
                marginTop: '6px',
                textAlign: 'center',
              }}
            >
              Apply For Company
            </Typography>
          </Box>
        </DialogTitle>

        {/* Dialog Apply CV */}
        <DialogContent>
          <form onSubmit={handleSubmit}>
            <Box display="flex" flexDirection="column" gap={2} mt={2}>
              <InputRow>
                <StyledTextField
                  required
                  label="Full Name"
                  name="FullName"
                  value={formData.FullName}
                  onChange={handleInputChange}
                  error={!!errors.FullName}
                  helperText={errors.FullName}
                  fullWidth
                />
                <StyledTextField
                  required
                  label="Phone Number"
                  name="PhoneNumber"
                  value={formData.PhoneNumber}
                  onChange={handleInputChange}
                  error={!!errors.PhoneNumber}
                  helperText={errors.PhoneNumber}
                  fullWidth
                />
                <StyledTextField
                  required
                  label="Email"
                  name="Email"
                  type="email"
                  value={formData.Email}
                  onChange={handleInputChange}
                  error={!!errors.Email}
                  helperText={errors.Email}
                  fullWidth
                />
              </InputRow>
              <Box mt={2}>
                <input
                  accept=".pdf"
                  style={{display: 'none'}}
                  id="cv-file"
                  type="file"
                  onChange={handleFileUpload}
                />
                <label htmlFor="cv-file">
                  <FileUploadButton component="span">
                    {formData.CvFile ? (
                      formData.CvFile.name
                    ) : (
                      <>
                        Attach CV File
                        <span style={{color: 'red', marginLeft: '5px'}}>*</span>
                      </>
                    )}
                  </FileUploadButton>
                </label>
                {errors.CvFile && (
                  <Typography color="error" variant="body2" mt={1}>
                    {errors.CvFile}
                  </Typography>
                )}
              </Box>
            </Box>
          </form>
        </DialogContent>
        <DialogActions sx={{justifyContent: 'center'}}>
          <SubmitButton onClick={handleSubmit}>Apply</SubmitButton>
        </DialogActions>
      </StyledDialog>

      {/* Dialog Details CV */}
      <StyledDialog
        open={detailsDialogOpen}
        onClose={handleCloseDetailsDialog}
        aria-labelledby="job-details-dialog"
        maxWidth="md"
        fullWidth
      >
        <DialogTitle id="job-details-dialog">
          <Box
            display="flex"
            justifyContent="space-between"
            alignItems="center"
          >
            <Typography variant="h4" component="h2" color={'steelblue'}>
              {selectedJob?.title}
            </Typography>
            <IconButton
              aria-label="close"
              onClick={handleCloseDetailsDialog}
              sx={{
                color: (theme) => theme.palette.grey[500],
              }}
            >
              <CloseIcon />
            </IconButton>
          </Box>
        </DialogTitle>
        <DialogContent>
          {selectedJob && (
            <>
              <Typography variant="h6" gutterBottom>
                Job Description
              </Typography>
              <Typography variant="body1" text-area>
                {selectedJob.description.split('\n').map((line, index) => (
                  <div key={index}>{line}</div>
                ))}
              </Typography>
              <Typography variant="h6" gutterBottom>
                Requirements
              </Typography>
              <Typography variant="body1" paragraph>
                {selectedJob.requirements.split('\n').map((line, index) => (
                  <div key={index}>{line}</div>
                ))}
              </Typography>
              <Typography variant="h6" gutterBottom>
                Benefits
              </Typography>
              <Typography variant="body1" paragraph>
                {selectedJob.benefits.split('\n').map((line, index) => (
                  <div key={index}>{line}</div>
                ))}
              </Typography>
              <Typography variant="h6" gutterBottom>
                Salary
              </Typography>
              <Typography variant="body1">
                {selectedJob.salary} $/year
              </Typography>
            </>
          )}
        </DialogContent>
      </StyledDialog>
    </>
  )
}

const Footer = ({dataHomePage}) => {
  return (
    <Box component="footer" sx={{bgcolor: 'background.paper', py: 6}}>
      <Container maxWidth="lg">
        <Box sx={{mb: 3}}>
          <Typography variant="h6" align="center" gutterBottom>
            Follow Us
          </Typography>
          <Box sx={{display: 'flex', justifyContent: 'center'}}>
            <IconButton aria-label="Instagram">
              <InstagramIcon />
            </IconButton>
            <IconButton aria-label="YouTube">
              <YouTubeIcon />
            </IconButton>
            <IconButton aria-label="Twitter">
              <TwitterIcon />
            </IconButton>
            <IconButton aria-label="Facebook">
              <FacebookIcon />
            </IconButton>
            <IconButton aria-label="LinkedIn">
              <LinkedInIcon />
            </IconButton>
          </Box>
        </Box>

        <Grid container spacing={4} justifyContent="space-evenly">
          {[
            {
              title: 'About Us',
              items: [
                `Address: ${dataHomePage.address}`,
                `Email: ${dataHomePage.email}`,
                `PhoneNumber: ${dataHomePage.phoneNumber}`,
              ],
            },
            {
              title: 'Policy',
              items: [
                'Application Security',
                'Software Principles',
                'Unwanted Software Policy',
                'Responsible Supply Chain',
                'Extended Workforce',
                'Community Guidelines',
                'How Our Business Works',
              ],
            },
            {
              title: 'Responsibility',
              items: [
                'Sustainability',
                'Crisis Response',
                'Diversity & Inclusion',
                'Accessibility',
                'Digital Wellbeing',
                'Safety Center',
              ],
            },
          ].map((section) => (
            <Grid item xs={12} sm={6} md={4} key={section.title}>
              <Typography variant="h6" color="text.primary" gutterBottom>
                {section.title}
              </Typography>
              <ul style={{listStyle: 'none', padding: 5}}>
                {section.items.map((item) => (
                  <li key={item}>
                    <Link href="#" variant="body2" color="text.secondary">
                      {item}
                    </Link>
                  </li>
                ))}
              </ul>
            </Grid>
          ))}
        </Grid>

        <Box mt={5}>
          <Typography variant="body2" color="text.secondary" align="center">
            {'Google '}
            <Link color="inherit" href="#">
              Trợ giúp
            </Link>
            {' | '}
            <Link color="inherit" href="#">
              Bảo mật
            </Link>
            {' | '}
            <Link color="inherit" href="#">
              Điều khoản
            </Link>
          </Typography>
        </Box>
      </Container>
    </Box>
  )
}

const ReasonBox = styled(Paper)(({theme, color, isSelected}) => ({
  padding: theme.spacing(2),
  textAlign: 'center',
  color: '#fff',
  backgroundColor: color,
  cursor: 'pointer',
  transition: 'all 0.3s ease',
  transform: isSelected ? 'scale(1.1)' : 'scale(1)',
  zIndex: isSelected ? 1 : 'auto',
  minHeight: '100px',
  '&:hover': {
    opacity: 0.9,
  },
}))

const ContentBox = styled(Box)(({theme}) => ({
  textAlign: 'left',
  marginTop: theme.spacing(2),
  padding: theme.spacing(10),
  backgroundColor: '#f5f5f5',
  borderRadius: theme.shape.borderRadius,
}))

const ReasonToChoose = ({reasons}) => {
  const [currentReason, setCurrentReason] = useState(0)
  return (
    <Box id="aboutUs" sx={{flexGrow: 1, p: 10, minHeight: '70vh'}}>
      <Typography
        variant="h4"
        align="center"
        gutterBottom
        style={{color: '#FF6B00', fontWeight: 'thin'}}
      >
        Reason for choosing us
      </Typography>
      <Container>
        <Grid container spacing={2} sx={{mb: 4, justifyContent: 'center'}}>
          {reasons.map((reason, index) => (
            <Grid item xs={3} key={reason.number}>
              <ReasonBox
                color={reason.color}
                onClick={() => setCurrentReason(index)}
                elevation={currentReason === index ? 8 : 1}
                isSelected={currentReason === index}
              >
                <Typography variant="h3">{index + 1}</Typography>
                <Typography variant="body1">{reason.title}</Typography>
              </ReasonBox>
            </Grid>
          ))}
        </Grid>
        <ContentBox sx={{backgroundColor: '#EEF1FC'}}>
          <Box
            sx={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
            }}
          >
            <Typography variant="h4" sx={{flexGrow: 1}}>
              {reasons[currentReason].number}
              {reasons[currentReason].title}
            </Typography>
          </Box>
          <Typography variant="h5" sx={{mt: 2, color: 'blue'}}>
            {reasons[currentReason].subTitle}
          </Typography>
          <Typography variant="body1" sx={{mt: 2}}>
            {reasons[currentReason].content}
          </Typography>
        </ContentBox>
      </Container>
    </Box>
  )
}

export default function Homepage1() {
  const [dataReasons, setDataReasons] = useState([])
  const [dataHomePage, setDataHomePage] = useState('')
  const [homePageAtiveId, setHomePageAtiveId] = useState('')
  const {refetch} = useGetReasons()
  const {refetch: refetchHomePage} = useGetHomePageActive()

  const fetchDataHomePage = useCallback(async () => {
    try {
      const response = await refetchHomePage()
      if (response && response?.data?.data) {
        setDataHomePage(response?.data?.data)
        setHomePageAtiveId(response?.data?.data?.id)
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetchHomePage])

  const fetchData = useCallback(async () => {
    try {
      const response = await refetch()
      if (response && Array.isArray(response?.data?.data?.items)) {
        setDataReasons(response?.data?.data?.items)
      }
    } catch (error) {
      console.error('Error fetching posts:', error)
    }
  }, [refetch])

  // const {data: dataImage} = useGetHomePageImage({
  //   id: homePageAtiveId,
  // })
  // console.log(dataImage)

  useEffect(() => {
    fetchData()
  }, [refetch, fetchData])
  useEffect(() => {
    fetchDataHomePage()
  }, [refetchHomePage, fetchDataHomePage])
  return (
    <>
      <Header dataHomePage={dataHomePage} homePageAtiveId={homePageAtiveId} />
      {dataHomePage.statusJobPost === 'Active' && <Recruitment />}
      {dataReasons.length > 0 && <ReasonToChoose reasons={dataReasons} />}
      <Footer dataHomePage={dataHomePage} />
    </>
  )
}
