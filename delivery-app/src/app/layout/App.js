import React from 'react'
import { Route } from 'react-router-dom'
import HomeLogin from '../../features/home/HomeLogin'

const App = () => {
  return (
    <div style={{display: 'flex', width: '100vw', height: '100vh'}}>
      <Route exact path="/" component={HomeLogin}/>
      <Route exact path="/(.+)"
      render={() => (
          <>
            App
          </>
      )}/>
    </div>
  )
}

export default App
