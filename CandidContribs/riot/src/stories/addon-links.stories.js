import { mount, compileNow } from '@storybook/riot';
import { linkTo } from '@storybook/addon-links';
import ButtonRaw from './Button.txt';

compileNow(ButtonRaw);

export default {
  title: 'Addon/Links',
};

export const GoToWelcome = () =>
  mount('my-button', {
    rounded: true,
    content: 'This button links to Welcome',
    value: 'with a parameter',
    handleClick: linkTo('Story/How to create a story', 'Welcome'),
  });

GoToWelcome.story = {
  name: 'Go to welcome',
};
