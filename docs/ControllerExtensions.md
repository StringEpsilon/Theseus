\> Namespace: Theseus <  

# class: ControllerExtensions



## Methods

### RedirectTo()

Shortcut for `Controller.Redirect(Url.To<TargetController>(actionExpression))`  

**Parameters:**   
| Name | Type | Description |
|----|----|----|
| <a name="controller">controller</a> | Controller | Controller instance on which to invoke the extension. |
| <a name="actionExpression">actionExpression</a> | Expression\<Func\<TargetController,Task\<ActionResult\>\>\> | Expression that identifies the target action. |

  
**Returns:** RedirectResult  
The constructed redirect result.

### RedirectTo()

Shortcut for `Controller.Redirect(Url.To<TargetController>(actionExpression))`  

**Parameters:**   
| Name | Type | Description |
|----|----|----|
| <a name="controller">controller</a> | Controller | Controller instance on which to invoke the extension. |
| <a name="actionExpression">actionExpression</a> | Expression\<Func\<TargetController,ActionResult\>\> | Expression that identifies the target action. |

  
**Returns:** RedirectResult  
The constructed redirect result.