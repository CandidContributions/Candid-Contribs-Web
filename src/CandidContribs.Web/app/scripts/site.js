import '@riotjs/hot-reload'
import { component } from 'riot'
import Schedule from './components/schedule.riot'
import Mushroomfield from './components/mushroomfield.riot'

if (document.getElementById('schedule') != null) {
    component(Schedule)(document.getElementById('schedule'), {});
}
if (document.getElementById('mushroom-field') != null) {
    component(Mushroomfield)(document.getElementById('mushroom-field'), {});
}