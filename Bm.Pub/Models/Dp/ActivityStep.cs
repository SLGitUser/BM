using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// �����
    /// </summary>
    [DisplayName("�����")]
    public sealed class ActivityStep : IId, IStamp
    {
        #region Implementation of IId

        /// <summary>
        /// ��ȡ�������ü�¼���
        /// </summary>
        /// <value>
        /// ��¼���
        /// </value>
        /// <remarks>
        /// ����洢ʱʹ���޷�������
        /// </remarks>
        [DisplayName("��¼���")]
        public long Id { get; set; }

        #endregion

        #region Implementation of ICreateStamp

        /// <summary>
        /// ��ȡ�������ü�¼������
        /// </summary>
        /// <value>
        /// ��¼������
        /// </value>
        /// <remarks>
        /// ����ʹ�ò����˵��˻���
        /// </remarks>
        [DisplayName("��¼������")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// ��ȡ�������ü�¼����ʱ��
        /// </summary>
        /// <value>
        /// ��¼����ʱ��
        /// </value>
        /// <remarks>
        /// ����ʹ�÷�����ʱ�䣬��Ҫʹ��Ĭ�Ͽ�ֵ
        /// </remarks>
        [DisplayName("��¼����ʱ��")]
        public DateTime CreatedAt { get; set; }

        #endregion

        #region Implementation of IUpdateStamp

        /// <summary>
        /// ��ȡ�������ü�¼������
        /// </summary>
        /// <value>
        /// ��¼������
        /// </value>
        /// <remarks>
        /// ����ʹ�ò����˵��˻�����Ϊ��ʱ��ʾ��¼�޸���
        /// </remarks>
        [DisplayName("��¼����޸���")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// ��ȡ�������ü�¼����ʱ��
        /// </summary>
        /// <value>
        /// ��¼����ʱ��
        /// </value>
        /// <remarks>
        /// Ϊ��ʱ��ʾ��¼�޸���
        /// </remarks>
        [DisplayName("��¼����޸�ʱ��")]
        public DateTime? UpdatedAt { get; set; }

        #endregion
        //MessageNo
        //UserNo
        //Type
        //At
        //LeaveAt
        //Remark
        /// <summary>
        /// ��ȡ����������Ϣ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ϣ���")]
        [StringLength(49)]
        [Required]
        public string MessageNo

        { get; set; }

        /// <summary>
        /// ��ȡ���������û����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�û����")]
        [StringLength(49)]
        [Required]
        public string UserNo
        { get; set; }

        /// <summary>
        /// ��ȡ�������û����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�����")]
        [StringLength(49)]
        [Required]
        public string Type
        { get; set; }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        [Required]
        public DateTime At { get; set; }

        /// <summary>
        /// �뿪ʱ��
        /// </summary>
        public DateTime? LeaveAtAt { get; set; }

        /// <summary>
        /// ��ȡ�������û��ע
        /// </summary>
        /// <remark></remark>
        [DisplayName("���ע")]
        [StringLength(49)]
        public string Remark

        { get; set; }
    }
}
