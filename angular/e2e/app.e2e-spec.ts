import { CrudAppProjectTemplatePage } from './app.po';

describe('CrudAppProject App', function() {
  let page: CrudAppProjectTemplatePage;

  beforeEach(() => {
    page = new CrudAppProjectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
