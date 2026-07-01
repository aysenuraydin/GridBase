import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { configureStore } from '@reduxjs/toolkit';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import rootReducer from './slices';
import { ProjectProvider } from 'context/ProjectContext';

const store = configureStore({ reducer: rootReducer, devTools: true });


export type RootState = ReturnType<typeof rootReducer>;

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <Provider store={store}>
    <React.Fragment>
      <BrowserRouter basename={process.env.PUBLIC_URL}>
        <ProjectProvider> 
            <App />
        </ProjectProvider>
      </BrowserRouter>
    </React.Fragment>
  </Provider>
);  