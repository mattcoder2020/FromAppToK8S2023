import { OrdermoduleModule } from './ordermodule.module';

describe('OrdermoduleModule', () => {
  let ordermoduleModule: OrdermoduleModule;

  beforeEach(() => {
    ordermoduleModule = new OrdermoduleModule();
  });

  it('should create an instance', () => {
    expect(ordermoduleModule).toBeTruthy();
  });
});
