import React, { useContext, useRef, useState } from 'react'
import {
  SDivider,
  SLink,
  SLinkContainer,
  SLinkIcon,
  SLinkLabel,
  SLinkNotification,
  SLogo,
  SSearch,
  SSearchIcon,
  SSidebar,
  SSidebarButton,
  STheme,
  SThemeLabel,
  SThemeToggler,
  SToggleThumb,
} from './styles'

import BugItLogo from '../images/BugItLogo.png'

import {
  AiOutlineApartment,
  AiOutlineHome,
  AiOutlineLeft,
  AiOutlineSearch,
  AiOutlineSetting,
  AiOutlinePlusSquare,
} from 'react-icons/ai'
import { MdLogout, MdOutlineAnalytics, MdLogin } from 'react-icons/md'
import { BsPeople } from 'react-icons/bs'

import { ThemeContext } from '../../App'
import { useLocation } from 'react-router-dom'
//import  DarkContext  from '../newHome'

const Sidebar = () => {
  const searchRef = useRef(null)
  const { setTheme, theme } = useContext(ThemeContext)
  //const [ BootStrapTableDark, setBootStrapTableDark ] = useState(false)
  const [sidebarOpen, setSidebarOpen] = useState(false)
  
  //const [DarkContex, setDarkContext] = useState(DarkContext)
  const { pathname } = useLocation()
  //const { setDarkTheme, dark } = useContext(DarkContext)
  const searchClickHandler = () => {
    if (!sidebarOpen) {
      setSidebarOpen(true)
      searchRef.current.focus()
    } else {
      // search functionality
    }
  }

  return (
    <SSidebar isOpen={sidebarOpen}>
      <>
        <SSidebarButton
          isOpen={sidebarOpen}
          onClick={() => setSidebarOpen((p) => !p)}
        >
          <AiOutlineLeft />
        </SSidebarButton>
      </>
      <SLogo>
        <img src={BugItLogo} alt='logo' />
      </SLogo>
      <SSearch
        onClick={searchClickHandler}
        style={!sidebarOpen ? { width: `fit-content` } : {}}
      >
        <SSearchIcon>
          <AiOutlineSearch />
        </SSearchIcon>
        <input
          ref={searchRef}
          placeholder='Search'
          style={!sidebarOpen ? { width: 0, padding: 0 } : {}}
        />
      </SSearch>
      <SDivider />
      {linksArray.map(({ icon, label, notification, to }) => (
        <SLinkContainer key={label} isActive={pathname === to}>
          <SLink to={to} style={!sidebarOpen ? { width: `fit-content` } : {}}>
            <SLinkIcon>{icon}</SLinkIcon>
            {sidebarOpen && (
              <>
                <SLinkLabel>{label}</SLinkLabel>
                {/* if notifications are at 0 or null, do not display */}
                {!!notification && (
                  <SLinkNotification>{notification}</SLinkNotification>
                )}
              </>
            )}
          </SLink>
        </SLinkContainer>
      ))}
      <SDivider />
      {secondaryLinksArray.map(({ icon, label, to }) => (
        <SLinkContainer key={label} isActive={pathname === to}>
          <SLink to={to} style={!sidebarOpen ? { width: `fit-content` } : {}}>
            <SLinkIcon>{icon}</SLinkIcon>
            {sidebarOpen && <SLinkLabel>{label}</SLinkLabel>}
          </SLink>
        </SLinkContainer>
      ))}
      <SDivider />
      <STheme>
        {sidebarOpen && <SThemeLabel>Dark Mode</SThemeLabel>}
        <SThemeToggler
          isActive={theme === 'dark'}
          onClick={() => {setTheme((p) => (p === 'light' ? 'dark' : 'light')); /*setDarkTheme((dark) => (dark = true))*/ }}
        >
          <SToggleThumb style={theme === 'dark' ? { right: '1px' } : {}} />
        </SThemeToggler>
      </STheme>
    </SSidebar>
  )
}

const linksArray = [
  {
    label: 'Home',
    icon: <AiOutlineHome />,
    to: '/newHome',
    notification: 0,
  },
  {
    label: 'Add Bug',
    icon: <AiOutlinePlusSquare />,
    to: '/CreateBug',
    notification: 0,
  },
  {
    label: 'Members',
    icon: <BsPeople />,
    to: '/MemberList',
    notification: 0,
  },
  {
    label: 'Projects',
    icon: <AiOutlineApartment />,
    to: '/HomePage',
    notification: 1,
  },
]

const secondaryLinksArray = [
  {
    label: 'Profile',
    icon: <AiOutlineSetting />,
    to: '/Profile',
  },
  {
    label: 'Login',
    icon: <MdLogin />,
    to: '/login',
  },
]

export default Sidebar
