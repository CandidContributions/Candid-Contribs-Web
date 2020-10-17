import '@riotjs/hot-reload'
import { component } from 'riot'
import Schedule from './components/schedule.riot'

if (document.getElementById('schedule') != null) {
    component(Schedule)(document.getElementById('schedule'), {});
}