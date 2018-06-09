import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { GameSetup } from './components/setup/GameSetup';
import { RegisterHuman } from './components/setup/RegisterHuman';
import { GameFull } from './components/setup/GameFull';
import { RegistrationWait } from './components/setup/RegistrationWait';
import { PlayPane } from './components/game/PlayPane';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata' component={ FetchData } />
    <Route path='/setup/create' component={ GameSetup } />
    <Route path='/setup/registerhuman' component={ RegisterHuman } />
    <Route path='/setup/gamefull' component={ GameFull } />
    <Route path='/setup/registrationwait' component={RegistrationWait} />
    <Route path='/game/playpane' component={ PlayPane } />
</Layout>;