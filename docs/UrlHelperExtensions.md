\> Namespace: Theseus <  

# class: IUrlHelperExtensions



## Methods

### To()

Generates a URL with an absolute path for the action method identified by the function expression.  

**Parameters:**   
| Name | Type | Description |
|----|----|----|
| <a name="urlHelper">urlHelper</a> | IUrlHelper | Extended IUrlHelper instance. |
| <a name="action">action</a> | Expression\<Func\<TController,ActionResult\>\> | Expression that identifies the target action. |
| <a name="routeValues">routeValues</a> | Object | Additional route values. |

  
**Returns:** String  
The generated URL.

### To()

Generates a URL with an absolute path for the action method identified by the function expression.  

**Parameters:**   
| Name | Type | Description |
|----|----|----|
| <a name="urlHelper">urlHelper</a> | IUrlHelper | Extended IUrlHelper instance. |
| <a name="action">action</a> | Expression\<Func\<TController,ActionResult\>\> | Expression that identifies the target action. |

  
**Returns:** String  
The generated URL.

### To()

Generates a URL with an absolute path for the action method identified by the function expression.  

**Parameters:**   
| Name | Type | Description |
|----|----|----|
| <a name="urlHelper">urlHelper</a> | IUrlHelper | Extended IUrlHelper instance. |
| <a name="action">action</a> | Expression\<Func\<TController,Task\<ActionResult\>\>\> | Expression that identifies the target action. |
| <a name="routeValues">routeValues</a> | Object | Additional route values. |

  
**Returns:** String  
The generated URL.

### To()

Generates a URL with an absolute path for the action method identified by the function expression.  

**Parameters:**   
| Name | Type | Description |
|----|----|----|
| <a name="urlHelper">urlHelper</a> | IUrlHelper | Extended IUrlHelper instance. |
| <a name="action">action</a> | Expression\<Func\<TController,Task\<ActionResult\>\>\> | Expression that identifies the target action. |

  
**Returns:** String  
The generated URL.