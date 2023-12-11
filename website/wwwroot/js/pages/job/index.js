const createJob = async (type) => {
    const url = getUrl(type)
    let sendData = {type: type};
    let response = await sendPostRequest(url, sendData)
    console.log(response)
}

const getUrl = type => {
    let lookup = {
        'fire': '/job/AddFireAndForgetJob',
        'delayed': '/job/AddDelayedJob',
        'recurring': '/job/AddRecurringJob',
    }
    return lookup[type]
}
const sendPostRequest = (url, data) => $.ajax({url: url, type: 'POST', data: data,})

