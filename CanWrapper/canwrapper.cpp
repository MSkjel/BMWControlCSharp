#include "canwrapper.h"

#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <sys/ioctl.h>
#include <sys/uio.h>
#include <net/if.h>
#include <unistd.h>
#include <linux/can.h>
#include <linux/can/raw.h>

#include <assert.h>

#include <errno.h>

#include <fcntl.h>

#include <stdio.h>

#include <signal.h>

#include <linux/can/error.h>

#include <sys/time.h>

#include <sys/select.h>

#define INVALID_SOCKET -1

extern "C"
{

	bool Init(const char *interfaceName, int &errorCode)
	{
		struct sockaddr_can addr;
		struct ifreq ifr;

		int ret;

		errorCode = 0;

		m_socket = socket(PF_CAN, SOCK_RAW, CAN_RAW);

		strcpy(ifr.ifr_name, interfaceName);
		ret = ioctl(m_socket, SIOCGIFINDEX, &ifr);

		if (ret)
		{
			errorCode = errno;

			return false;
		}

		addr.can_family = AF_CAN;
		addr.can_ifindex = ifr.ifr_ifindex;

		ret = bind(m_socket, (struct sockaddr *)&addr, sizeof(addr));

		if (ret)
		{
			errorCode = errno;

			close(m_socket);
			m_socket = INVALID_SOCKET;

			return false;
		}

		m_initialized = true;

		return true;
	}

	void Close()
	{
		if (m_initialized)
		{
			shutdown(m_socket, SHUT_RDWR);

			close(m_socket);

			m_socket = INVALID_SOCKET;

			m_initialized = false;
		}
	}

	bool SendMsg(struct can_frame msg, bool extended, bool rtr, int &errorCode)
	{
		int res;

		errorCode = 0;

		if (!m_initialized)
			return false;


		if (extended)
			msg.can_id |= CAN_EFF_FLAG;

		if (rtr)
			msg.can_id |= CAN_RTR_FLAG;

		res = write(m_socket, &msg, sizeof(struct can_frame));

		if (res < 0)
		{
			errorCode = errno;

			return false;
		}

		return true;
	}

	bool GetMsg(struct can_frame &frame, bool &extended, bool &rtr, bool &error, int &errorCode, struct timeval timeout)
	{
		int bytesRead;
		int ret;
		fd_set rfds;

		errorCode = 0;

		if (!m_initialized)
			return false;

		FD_ZERO(&rfds);
		FD_SET(m_socket, &rfds);

		ret = select(m_socket + 1, &rfds, NULL, NULL, &timeout);
		if (ret < 0)
		{
			errorCode = errno;

			return false;
		}

		if (ret > 0)
		{
			bytesRead = read(m_socket, &frame, sizeof(frame));

			if (bytesRead < 0)
			{
				errorCode = errno;

				return false;
			}

			if (bytesRead == sizeof(frame))
			{
				error = frame.can_id & CAN_ERR_FLAG;

				extended = frame.can_id & CAN_EFF_FLAG;

				rtr = frame.can_id & CAN_RTR_FLAG;

				if (error)
					frame.can_id &= CAN_ERR_MASK;

				if (extended)
					frame.can_id &= CAN_EFF_MASK;
				else
					frame.can_id &= CAN_SFF_MASK;

				return true;
			}
		}

		return false;
	}

	bool SetRecvBufferSize(int size)
	{
		int ret;
		int rcvbuf_size = size;

		ret = setsockopt(m_socket, SOL_SOCKET, SO_RCVBUF, &rcvbuf_size, sizeof(rcvbuf_size));

		if (ret < 0)
			return false;

		return true;
	}

	void EnableErrorMessages()
	{
		int ret;

		can_err_mask_t err_mask = (CAN_ERR_TX_TIMEOUT | CAN_ERR_BUSOFF);

		ret = setsockopt(m_socket, SOL_CAN_RAW, CAN_RAW_ERR_FILTER,
			&err_mask, sizeof(err_mask));
	}
}