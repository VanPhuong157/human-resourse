import React, {useState, useCallback, useEffect} from 'react'
import { rootApi } from '../../api/rootApi'
import path from '../../api/path'
import bannerImage from '../../assets/images/homepage/banner2.png'
import {useGetPosts} from './request/JobListRequest'

import {
  styled,
  Typography,
  Button,
  Container,
  Grid,
  Card,
  CardContent,
  CardActions,
  Link,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Box,
  IconButton,
} from '@mui/material'
import AccessTimeIcon from '@mui/icons-material/AccessTime'
import LocationOnIcon from '@mui/icons-material/LocationOn'
import CloseIcon from '@mui/icons-material/Close'
import PaidIcon from '@mui/icons-material/Paid'
import NavigateNextIcon from '@mui/icons-material/NavigateNext'
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore'
import {showError, showSuccess} from '../../components/notification'
import {useNavigate} from 'react-router-dom'
const Header = styled('header')(({theme}) => ({
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-between',
  marginRight: '450px',
  padding: theme.spacing(1, 10),
  backgroundColor: theme.palette.background.paper,
  [theme.breakpoints.down('md')]: {
    padding: theme.spacing(1, 2.5),
    flexWrap: 'wrap',
  },
}))

const Logo = styled('img')({
  aspectRatio: '5.56',
  width: 'auto',
})

const Nav = styled('nav')(({theme}) => ({
  display: 'flex',
  gap: theme.spacing(15.5),
}))

const NavLink = styled(Link)(({theme}) => ({
  color: theme.palette.primary.main,
  fontWeight: 700,
  fontSize: '16px',
  letterSpacing: '0.15px',
  textDecoration: 'none',
}))

const ApplyButton = styled(Button)(({theme}) => ({
  borderRadius: '20px',
  backgroundColor: '#0056b3',
  color: theme.palette.common.white,
  padding: theme.spacing(1.25, 2.875),
  fontWeight: 700,
  fontSize: '20px',
  lineHeight: 1.5,
  textTransform: 'none',
  '&:hover': {
    backgroundColor: theme.palette.warning.dark,
  },
}))

const HeroBanner = styled('img')({
  aspectRatio: '4',
  height: '100%',
  width: '100%',
  objectFit: 'fill',
})

const MainContent = styled('main')(({theme}) => ({
  backgroundColor: theme.palette.grey[300],
  padding: theme.spacing(5, 0),
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
  [theme.breakpoints.down('md')]: {
    padding: theme.spacing(5, 2.5),
  },
}))

const StyledContainer = styled(Container)({
  display: 'flex',
  alignItems: 'center',
})

const SectionTitle = styled(Typography)(({theme}) => ({
  color: theme.palette.warning.main,
  fontWeight: 700,
  fontSize: '52px',
  lineHeight: '38%',
  letterSpacing: '0.15px',
  [theme.breakpoints.down('md')]: {
    fontSize: '40px',
  },
}))

const SectionSubtitle = styled(Typography)(({theme}) => ({
  color: theme.palette.text.primary,
  fontWeight: 700,
  fontSize: '23px',
  lineHeight: '87%',
  letterSpacing: '0.15px',
  marginTop: theme.spacing(4.625),
}))

const JobCard = styled(Card)(({theme}) => ({
  borderRadius: '20px',
  height: '222px',
  width: '100%',
}))

const JobTitle = styled(Typography)(({theme}) => ({
  color: theme.palette.text.primary,
  fontWeight: 700,
  fontSize: '18px',
  lineHeight: '111%',
  letterSpacing: '0.15px',
}))

const Divider = styled('hr')(({theme}) => ({
  border: 'none',
  height: '1px',
  backgroundColor: theme.palette.warning.light,
  margin: theme.spacing(1, 0),
}))

const JobDetail = styled('div')(({theme}) => ({
  display: 'flex',
  alignItems: 'center',
  gap: theme.spacing(1.25),
  color: theme.palette.text.secondary,
  fontSize: '16px',
  lineHeight: 1.5,
  letterSpacing: '0.15px',
  marginTop: theme.spacing(1),
}))

const DetailsLink = styled(Link)(({theme}) => ({
  color: theme.palette.info.main,
  marginLeft: '20px',
  marginRight: '80px',
  fontStyle: 'italic',
  fontWeight: 500,
  fontSize: '15px',
  lineHeight: 1.6,
  textDecoration: 'underline',
  cursor: 'pointer',
}))

const ApplyNowButton = styled(Button)(({theme}) => ({
  borderRadius: '20px',
  border: `1px solid ${theme.palette.warning.main}`,
  backgroundColor: 'rgba(255, 255, 255, 0.99)',
  color: theme.palette.warning.main,
  padding: theme.spacing(1.125, 2.25),
  fontWeight: 700,
  fontSize: '16px',
  lineHeight: 1.5,
  textTransform: 'none',
  '&:hover': {
    color: '#fff',
    backgroundColor: theme.palette.warning.light,
  },
}))

const AboutSection = styled('section')(({theme}) => ({
  display: 'flex',
  gap: theme.spacing(2.5),
  padding: theme.spacing(5.5, 10),
  [theme.breakpoints.down('md')]: {
    flexDirection: 'column',
    padding: theme.spacing(5.5, 2.5),
  },
}))

const AboutImage = styled('img')(({theme}) => ({
  aspectRatio: '1.41',
  width: '50%',
  objectFit: 'cover',
  [theme.breakpoints.down('md')]: {
    width: '100%',
  },
}))

const AboutContent = styled('div')(({theme}) => ({
  width: '50%',
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'center',
  [theme.breakpoints.down('md')]: {
    width: '100%',
  },
}))

const AboutTitle = styled(Typography)(({theme}) => ({
  color: theme.palette.warning.main,
  fontWeight: 700,
  fontSize: '51px',
  lineHeight: 1.2,
  letterSpacing: '0.15px',
  marginBottom: theme.spacing(2),
  [theme.breakpoints.down('md')]: {
    fontSize: '40px',
  },
}))

const AboutDescription = styled(Typography)(({theme}) => ({
  color: theme.palette.text.primary,
  fontSize: '14px',
  lineHeight: 1.5,
}))

const StyledDialog = styled(Dialog)(({theme}) => ({
  '& .MuiDialog-paper': {
    borderRadius: '16px',
  },
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
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
  },
}))

const FileUploadButton = styled(Button)(({theme}) => ({
  borderRadius: '54px',
  border: `1px solid ${theme.palette.text.primary}`,
  color: '#12448a',
  fontWeight: 200,
  width: '100%',
  justifyContent: 'flex-start',
  padding: '7px 12px',
  marginTop: '13px',
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

const CustomNavigateBeforeIcon = styled(NavigateBeforeIcon)(({theme}) => ({
  width: '45px',
  height: '50px',
  '&:hover': {
    color: '#fff',
  },
}))
const CustomNavigateNextIcon = styled(NavigateNextIcon)(({theme}) => ({
  width: '45px',
  height: '50px',
  '&:hover': {
    color: '#fff',
  },
}))

function HomePage() {
  const [dialogOpen, setDialogOpen] = useState(false)
  const [detailsDialogOpen, setDetailsDialogOpen] = useState(false)
  const [selectedJob, setSelectedJob] = useState(null)
  const navigate = useNavigate()
  const [errors, setErrors] = useState({})

  const handleLoginClick = () => {
    navigate('/login')
  }

  const [formData, setFormData] = useState({
    FullName: '',
    PhoneNumber: '',
    Email: '',
    CvFile: null,
  })

  const [job] = useState([])
  const [currentSlide, setCurrentSlide] = useState(0)
  const [jobsInSlide, setJobsInSlide] = useState([])
  const PageSize = 6

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

  const JobListing = ({job, onApply, onDetails}) => {
    const ruleTitle = (title, maxLength) => {
      if (title.length > maxLength) {
        return title.substring(0, maxLength) + '...'
      }
      return title
    }

    return (
      <Grid item xs={12} sm={6} md={4}>
        <JobCard>
          {job && (
            <CardContent>
              <JobTitle variant="h3">{ruleTitle(job.title, 20)}</JobTitle>
              <Divider />
              <JobDetail>
                <AccessTimeIcon />
                <span>
                  {job.experienceYear === 0
                    ? 'Không yêu cầu kinh nghiệm'
                    : `${job.experienceYear} years of experience`}
                </span>{' '}
              </JobDetail>
              <JobDetail>
                <PaidIcon />
                <span>
                  {job.salary === 0
                    ? 'Lương theo thỏa thuận'
                    : `${job.salary} $/year`}
                </span>{' '}
              </JobDetail>
              <JobDetail>
                <LocationOnIcon />
                <span>{job.departmentName}</span>
              </JobDetail>
            </CardContent>
          )}
          <CardActions>
            <DetailsLink onClick={() => onDetails(job)} tabIndex={0}>
              Chi tiết
            </DetailsLink>
            <ApplyNowButton onClick={() => onApply(job)} tabIndex={0}>
              Ứng tuyển ngay
            </ApplyNowButton>
          </CardActions>
        </JobCard>
      </Grid>
    )
  }
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

  const handleInputChange = (event) => {
    const {name, value} = event.target
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }))
    setErrors({...errors, [name]: !value})
  }

  const handleFileUpload = (event) => {
    const file = event.target.files[0]
    setFormData((prevData) => ({
      ...prevData,
      CvFile: file,
    }))
  }

  const handleSubmit = async (event) => {
    event.preventDefault()

    //Submit FormData
    const formDataToSend = new FormData()
    formDataToSend.append('FullName', formData.FullName)
    formDataToSend.append('PhoneNumber', formData.PhoneNumber)
    formDataToSend.append('Email', formData.Email)
    formDataToSend.append('CvFile', formData.CvFile)
    formDataToSend.append('JobPostId', selectedJob.id)
    try {
      const response = await rootApi.post(
        path.applicant.applyCv(),
        formDataToSend,
        {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        },
      )
      showSuccess({message: response.data?.message})
      handleCloseDialog()
    } catch (error) {
      console.log('error', error)

      if (error.response?.status === '') {
        const badRequestMessage = error.response?.data?.message
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
      <Header>
        <Logo
          src="https://cdn.builder.io/api/v1/image/assets/TEMP/a9bf173f64c6fdd796ee72a35fd49e3fccf14fe8762a5776df9308333834480f?apiKey=052410c29b764503af7d0f1a6991d23d&"
          alt="Company logo"
        />
        <Nav>
          <NavLink href="#" tabIndex={0}>
            Vị trí tuyển dụng
          </NavLink>
          <NavLink href="#" tabIndex={0}>
            Về chúng tôi
          </NavLink>
          <NavLink href="#" tabIndex={0}>
            Liên hệ
          </NavLink>
        </Nav>
        <ApplyButton onClick={handleLoginClick}>Login</ApplyButton>
      </Header>
      <HeroBanner src={bannerImage} alt="Career opportunities banner" />
      <MainContent>
        <SectionTitle variant="h1">Cơ hội nghề nghiệp</SectionTitle>
        <SectionSubtitle variant="h2">dành riêng cho bạn</SectionSubtitle>
        <StyledContainer maxWidth="lg">
          <CustomNavigateBeforeIcon
            variant="contained"
            disabled={currentSlide === 0}
            onClick={handlePrevSlide}
            sx={{visibility: currentSlide === 0 ? 'hidden' : 'visible'}}
          ></CustomNavigateBeforeIcon>
          <Grid container spacing={3.5} sx={{mt: 3.375}}>
            {jobsInSlide.map((job, index) => (
              <JobListing
                key={index}
                job={job}
                onApply={() => handleOpenDialog(job)}
                onDetails={() => handleOpenDetailsDialog(job)}
              />
            ))}
          </Grid>
          <CustomNavigateNextIcon
            variant="contained"
            disabled={currentSlide === Math.floor(job.length / PageSize) - 1}
            onClick={handleNextSlide}
            sx={{
              visibility: jobsInSlide.length < PageSize ? 'hidden' : 'visible',
            }}
          ></CustomNavigateNextIcon>
        </StyledContainer>
      </MainContent>
      <AboutSection>
        <AboutImage
          src="https://doanhnhanplus.vn/wp-content/uploads/2020/05/Happy-at-work.jpg"
          alt="About us image"
        />
        <AboutContent>
          <AboutTitle variant="h2">
            Đồng hành <br /> cùng chúng tôi
          </AboutTitle>
          <AboutDescription>
            SHRS là Dịch vụ quản lý nhân sự về CNTT hàng đầu Toàn cầu với doanh
            thu 1 tỷ USD được báo cáo vào năm 2023. Với đội ngũ nhân sự hơn
            30.000 chuyên gia và kỹ sư CNTT đang làm việc tại hơn 30 quốc gia
            trên thế giới, cùng với các trung tâm phát triển toàn cầu, FPT
            Software là đối tác đáng tin cậy của hơn 1.000 khách hàng, trong đó
            hơn 100 khách hàng có tên trong Fortune 500. Chúng tôi tự tin mang
            đến cơ hội làm việc trong môi trường đa ngôn ngữ, trải nghiệm hành
            trình sự nghiệp toàn diện, thách thức nhưng đầy thú vị. Hãy cùng bắt
            đầu hành trình rực rỡ của bạn cùng với SHRS ngay hôm nay thôi nào!
          </AboutDescription>
        </AboutContent>
      </AboutSection>

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
              START WITH SHRS COMPANY
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
                  fullWidth
                />
                <StyledTextField
                  required
                  label="Phone Number"
                  name="PhoneNumber"
                  value={formData.PhoneNumber}
                  onChange={handleInputChange}
                  fullWidth
                />
                <StyledTextField
                  required
                  label="Email"
                  name="Email"
                  type="email"
                  value={formData.Email}
                  onChange={handleInputChange}
                  fullWidth
                />
              </InputRow>
              <Box mt={2}>
                <Typography variant="body1" fontStyle="italic">
                  Attach file CV
                </Typography>
                <input
                  accept=".pdf"
                  style={{display: 'none'}}
                  id="cv-file"
                  type="file"
                  onChange={handleFileUpload}
                />
                <label htmlFor="cv-file">
                  <FileUploadButton component="span">
                    {formData.CvFile ? formData.CvFile.name : 'Tải lên'}
                  </FileUploadButton>
                </label>
              </Box>
            </Box>
          </form>
        </DialogContent>
        <DialogActions sx={{justifyContent: 'center'}}>
          <SubmitButton onClick={handleSubmit}>Ứng tuyển</SubmitButton>
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
              <Typography variant="body1" paragraph>
                {selectedJob.description}
              </Typography>
              <Typography variant="h6" gutterBottom>
                Requirements
              </Typography>
              <Typography variant="body1" paragraph>
                {selectedJob.requirements}
              </Typography>
              <Typography variant="h6" gutterBottom>
                Benefits
              </Typography>
              <Typography variant="body1" paragraph>
                {selectedJob.benefits}
              </Typography>
              <Typography variant="h6" gutterBottom>
                Salary
              </Typography>
              <Typography variant="body1">{selectedJob.salary}</Typography>
            </>
          )}
        </DialogContent>
      </StyledDialog>
    </>
  )
}

export default HomePage
